using Microsoft.EntityFrameworkCore;
using OnlineSecureHospitalSystem.Data;
using OnlineSecureHospitalSystem.Data.Models;

namespace OnlineSecureHospitalSystem.Services.Patient
{
    public class PatientService : IPatientService
    {
        private readonly AppDbContext _appDbContext;

        public PatientService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Appointments>> GetAppointmentsAsync(int userId)
        {
            var patient = await GetPatientByUserIdAsync(userId);

            return await _appDbContext.Appointments
                .Where(a => a.Patient_ID == patient!.Patient_ID)
                .Include(a => a.Doctor).ThenInclude(d => d!.User)
                .ToListAsync();
        }

        public async Task<Patients?> GetPatientByUserIdAsync(int userId)
        {
            return await _appDbContext.Patients
                .Include(u => u.User)
                .FirstOrDefaultAsync(p => p.User_ID == userId);
        }

        public async Task<bool> BookAppointmentAsync(int userId, Appointments appointment)
        {
            var patient = await GetPatientByUserIdAsync(userId);

            appointment.Patient_ID = patient!.Patient_ID;

            _appDbContext.Appointments.Add(appointment);

            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAppointmentAsync(int appointmentId, string reason, string extraInformation)
        {
            var appointment = await _appDbContext.Appointments
                .FirstOrDefaultAsync(a => a.Appointment_ID == appointmentId);

            appointment!.Reason = reason;
            appointment.Extra_Information = extraInformation;

            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelAppointmentAsync(int appointmentId)
        {
            var appointment = await _appDbContext.Appointments
                .FirstOrDefaultAsync(a => a.Appointment_ID == appointmentId);

            appointment!.Appointment_Status = "Cancelled";

            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
