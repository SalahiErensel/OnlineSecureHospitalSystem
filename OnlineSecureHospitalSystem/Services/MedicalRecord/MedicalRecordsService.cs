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

            return await _appDbContext.MedicalRecords
                .Where(mr => mr.Patient_ID == patient!.Patient_ID)
                .Include(mr => mr.Doctor).ThenInclude(d => d!.User).Include(mr => mr.Appointment)
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
    }
}
