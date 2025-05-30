using OnlineSecureHospitalSystem.Data.Models;

namespace OnlineSecureHospitalSystem.Services.Admin
{
    public interface IAdminService
    {
        Task<List<Users>> GetAllUsersAsync();
        Task<bool> RegisterUserAsync(Users user, string? specialization, DateTime? dob);
    }
}
