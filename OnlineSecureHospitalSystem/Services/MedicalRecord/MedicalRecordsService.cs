using Microsoft.EntityFrameworkCore;
using OnlineSecureHospitalSystem.Data;
using OnlineSecureHospitalSystem.Data.Models;

namespace OnlineSecureHospitalSystem.Services.MedicalRecord
{
    public class MedicalRecordsService : IMedicalRecordsService
    {
        private readonly AppDbContext _appDbContext;

        public MedicalRecordsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<MedicalRecords>> GetOwnMedicalRecordsByUserIdAsync(int userId)
        {
            var patient = await _appDbContext.Patients
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.User_ID == userId);

            if (patient == null) return new List<MedicalRecords>();

            return await _appDbContext.MedicalRecords
                .Where(mr => mr.Patient_ID == patient.Patient_ID)
                .Include(mr => mr.Doctor).ThenInclude(d => d!.User)
                .Include(mr => mr.Appointment)
                .ToListAsync();
        }

        public async Task<List<MedicalRecords>> GetMedicalRecordsByPatientIdAsync(int patientId)
        {
            return await _appDbContext.MedicalRecords
                .Where(mr => mr.Patient_ID == patientId)
                .Include(mr => mr.Doctor).ThenInclude(d => d!.User)
                .Include(mr => mr.Appointment)
                .ToListAsync();
        }

        public async Task<bool> HasMedicalRecordForAppointmentAsync(int appointmentId)
        {
            return await _appDbContext.MedicalRecords
                .AnyAsync(mr => mr.Appointment_ID == appointmentId);
        }

        public async Task<MedicalRecords?> GetMedicalRecordByAppointmentIdAsync(int appointmentId)
        {
            return await _appDbContext.MedicalRecords
                .Include(mr => mr.Doctor).ThenInclude(d => d!.User)
                .Include(mr => mr.Patient).ThenInclude(p => p!.User)
                .Include(mr => mr.Appointment)
                .FirstOrDefaultAsync(mr => mr.Appointment_ID == appointmentId);
        }

        public async Task<List<MedicalRecords>> GetMedicalRecordsByDoctorAsync(int doctorId)
        {
            return await _appDbContext.MedicalRecords
                .Where(mr => mr.Curing_Doctor_ID == doctorId)
                .Include(mr => mr.Patient).ThenInclude(p => p!.User)
                .Include(mr => mr.Doctor).ThenInclude(d => d!.User)
                .Include(mr => mr.Appointment)
                .OrderByDescending(mr => mr.Appointment!.Appointment_Date)
                .ToListAsync();
        }

        public async Task<bool> CreateMedicalRecordAsync(MedicalRecords medicalRecord)
        {
            try
            {
                var appointment = await _appDbContext.Appointments
                    .FirstOrDefaultAsync(a => a.Appointment_ID == medicalRecord.Appointment_ID
                                            && a.Doctor_ID == medicalRecord.Curing_Doctor_ID
                                            && a.Appointment_Status == "Completed");

                if (appointment == null)
                    return false;

                var existingRecord = await _appDbContext.MedicalRecords
                    .FirstOrDefaultAsync(mr => mr.Appointment_ID == medicalRecord.Appointment_ID);

                if (existingRecord != null)
                    return false;

                _appDbContext.MedicalRecords.Add(medicalRecord);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateMedicalRecordAsync(int appointmentId, int doctorId, byte[] encryptedData, string signature, bool isSensitive)
        {
            try
            {
                var medicalRecord = await _appDbContext.MedicalRecords
                    .FirstOrDefaultAsync(mr => mr.Appointment_ID == appointmentId
                                             && mr.Curing_Doctor_ID == doctorId);

                if (medicalRecord == null)
                    return false;

                medicalRecord.Record_Data = encryptedData;
                medicalRecord.Signature = signature;
                medicalRecord.Is_Sensitive = isSensitive;

                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<int> GetMedicalRecordsCountByDoctorAsync(int doctorId)
        {
            return await _appDbContext.MedicalRecords
                .Where(mr => mr.Curing_Doctor_ID == doctorId)
                .CountAsync();
        }
    }
}