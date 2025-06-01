using OnlineSecureHospitalSystem.Data.DTO;
using OnlineSecureHospitalSystem.Data.Models;

namespace OnlineSecureHospitalSystem.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<List<Roles>> GetRolesAsync();
        Task<AuthenticatedUser?> LoginUserAsync(LoginDTO loginDTO);
        Task<bool> UpdatePasswordAsync(int userId, string newPassword);
        Task<bool> RegisterPatientAsync(RegisterPatientDTO registerPatient);
        Task<Users?> GetUserByIdAsync(int userId);
    }
}