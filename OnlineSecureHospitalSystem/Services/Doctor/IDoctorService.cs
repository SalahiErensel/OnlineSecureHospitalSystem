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
        Task<List<Assignments>> GetAssignmentByDoctorIdAsync(int doctorId);
        Task<DateTime?> GetLastAppointmentDateAsync(int patientId, int doctorId);

        Task<List<Appointments>> GetAppointmentsByDoctorAndStatusAsync(int doctorId, string status);
        Task<List<Appointments>> GetAllAppointmentsByDoctorAsync(int doctorId);
        Task<List<Patients>> GetPatientsByDoctorAsync(int doctorId);
        Task<bool> ScheduleAppointmentAsync(int appointmentId, DateTime scheduledDateTime);
        Task<bool> CompleteAppointmentAsync(int appointmentId);
        Task<List<Appointments>> GetPatientAppointmentsWithDoctorAsync(int patientId, int doctorId);

        Task<int> GetPendingAppointmentsCountAsync(int doctorId);
        Task<int> GetScheduledAppointmentsCountAsync(int doctorId);
        Task<int> GetCompletedAppointmentsCountAsync(int doctorId);
        Task<int> GetPatientsCountAsync(int doctorId);
        Task<int> GetMedicalRecordsCountAsync(int doctorId);
        Task<List<Doctors>> GetConsultingDoctorsAsync();
        Task<bool> AssignConsultingDoctorAsync(int patientId, int assignerCuringDoctorId, int consultingDoctorId);
    }
}