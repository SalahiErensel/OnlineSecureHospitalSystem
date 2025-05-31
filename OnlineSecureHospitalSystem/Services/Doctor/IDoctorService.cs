using OnlineSecureHospitalSystem.Data.Models;

namespace OnlineSecureHospitalSystem.Services.Doctor
{
    public interface IDoctorService
    {
        Task<List<Patients>> GetAllPatientsAsync();
    }
}
