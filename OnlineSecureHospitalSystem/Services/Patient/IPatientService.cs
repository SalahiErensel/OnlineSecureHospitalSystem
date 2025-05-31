using OnlineSecureHospitalSystem.Data.Models;

namespace OnlineSecureHospitalSystem.Services.Patient
{
    public interface IPatientService
    {
        Task<List<Appointments>> GetAppointmentsAsync(int userId);
        Task<Patients?> GetPatientByUserIdAsync(int userId);
        Task<bool> BookAppointmentAsync(int userId, Appointments appointment);
        Task<bool> UpdateAppointmentAsync(int appointmentId, string reason, string extraInformation);

        Task<bool> CancelAppointmentAsync(int appointmentId);
    }
}
