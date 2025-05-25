using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineSecureHospitalSystem.Data;
using OnlineSecureHospitalSystem.Data.Models;

namespace OnlineSecureHospitalSystem.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHasher<Users> _passwordHasher;

        //Constructor
        public AuthenticationService(AppDbContext appDbContext, IPasswordHasher<Users> passwordHasher)
        {
            _appDbContext = appDbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<List<Roles>> GetRolesAsync()
        {
            return await _appDbContext.Roles.ToListAsync();
        }

        public async Task<bool> RegisterUserAsync(Users user)
        {
            //Check if username is already used or not
            if (await _appDbContext.Users.AnyAsync(x => x.Username == user.Username))
            {
                return false;
            }

            var newUser = new Users
            {
                Username = user.Username,
                Role_ID = user.Role_ID,
                IsDefaultPassword = 1,
                Password = "123456",
            };

            //Hashing password
            newUser.Password = _passwordHasher.HashPassword(newUser, newUser.Password);

            //Saving to database
            _appDbContext.Users.Add(newUser);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<AuthenticatedUser?> LoginUserAsync(Users user)
        {
            var existingUser = await _appDbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Username == user.Username);

            if (existingUser == null)
            {
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password!, user.Password!);

            if (result == PasswordVerificationResult.Success)
            {
                return new AuthenticatedUser
                {
                    User_ID = existingUser.User_ID,
                    Username = existingUser.Username!,
                    RoleName = existingUser.Role!.Role_Name!,
                    IsDefaultPassword = existingUser.IsDefaultPassword!.Value,
                };
            }

            return null;
        }
    }
}