using OnlineSecureHospitalSystem.Data.Models;

namespace OnlineSecureHospitalSystem.Services.Doctor
{
    public interface IDoctorService
    {
        Task<List<Patients>> GetAllPatientsAsync();
        Task<int> GetTotalPendingAssignmentsAsync();
        Task<int> GetTotalPatientsAsync();
        Task<int> GetTotalDoctorsAsync();
        Task<List<Appointments>> GetPendingAppointmentsAsync();
        Task<List<Doctors>> GetToBeAssignedDoctorsAsync();
        Task<bool> AssignCuringDoctorAsync(Assignments assignment);
        Task<Doctors?> GetDoctorByUserIdAsync(int userId);
    }
}