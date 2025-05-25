using OnlineSecureHospitalSystem.Data.Models;

namespace OnlineSecureHospitalSystem.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<List<Roles>> GetRolesAsync();
        Task<bool> RegisterUserAsync(Users user);

        Task<AuthenticatedUser?> LoginUserAsync(Users user);
    }
}
