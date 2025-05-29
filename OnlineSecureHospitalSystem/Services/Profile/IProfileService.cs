using OnlineSecureHospitalSystem.Data.DTO;

namespace OnlineSecureHospitalSystem.Services.Profile
{
    public interface IProfileService
    {
        Task<DateTime> GetPatientDOBAsync(int userId);
        Task<string> GetDoctorSpecializationAsync(int userId);

        Task<bool> UpdateProfileAsync(UpdateProfileDTO updateProfileDto);
    }
}