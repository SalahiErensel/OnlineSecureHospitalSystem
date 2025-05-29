using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineSecureHospitalSystem.Data;
using OnlineSecureHospitalSystem.Data.DTO;
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

        public async Task<bool> RegisterUserAsync(Users user, string? specialization, DateTime? dob)
        {
            //Check if username is already used or not
            if (await _appDbContext.Users.AnyAsync(x => x.Username == user.Username))
            {
                return false;
            }

            var newUser = new Users
            {
                Address = user.Address,
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Phone_Number = user.Phone_Number,
                Email = user.Email,
                Password = "123456", //This will be hashed later
                Username = user.Username,
                IsDefaultPassword = 1,
                Role_ID = user.Role_ID
            };

            //Hashing password
            newUser.Password = _passwordHasher.HashPassword(newUser, newUser.Password);

            //Saving to database
            _appDbContext.Users.Add(newUser);

            await _appDbContext.SaveChangesAsync();

            if (new[] { 3, 4, 5 }.Contains(user.Role_ID))
            {
                int isChiefDoctor = user.Role_ID == 3 ? 1 : 0;

                var doctor = new Doctors
                {
                    User_ID = newUser.User_ID,
                    Is_Chief = isChiefDoctor,
                    Specialization = specialization,
                };

                _appDbContext.Doctors.Add(doctor);
            }

            if (new[] { 6 }.Contains(user.Role_ID))
            {
                var patient = new Patients
                {
                    User_ID = newUser.User_ID,
                    DOB = dob,
                };
                _appDbContext.Patients.Add(patient);
            }

            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<AuthenticatedUser?> LoginUserAsync(LoginDTO loginDTO)
        {
            var existingUser = await _appDbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Username == loginDTO.Username);

            if (existingUser == null)
            {
                return null;
            }

            if(existingUser.User_ID == 1)
            {
                //If the user is the default admin, return immediately
                return new AuthenticatedUser
                {
                    User_ID = existingUser.User_ID,
                    Username = existingUser.Username!,
                    RoleName = existingUser.Role!.Role_Name!,
                    IsDefaultPassword = existingUser.IsDefaultPassword!.Value,
                };
            }

            var result = _passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password!, loginDTO.Password!);

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

        public async Task<bool> UpdatePasswordAsync(int userId, string newPassword)
        {
            try
            {
                //Get the user from database
                var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.User_ID == userId);
                if (user == null)
                {
                    return false;
                }

                //Hash the new password and update
                user.Password = _passwordHasher.HashPassword(user, newPassword);
                user.IsDefaultPassword = 0; //Mark as no longer default password

                _appDbContext.Users.Update(user);
                await _appDbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RegisterPatientAsync(RegisterPatientDTO registerPatient)
        {
            //Check if username is already taken
            if (await _appDbContext.Users.AnyAsync(x => x.Username == registerPatient.Username))
            {
                return false;
            }

            var newUser = new Users
            {
                Address = registerPatient.Address,
                Email = registerPatient.Email,
                First_Name = registerPatient.First_Name,
                Last_Name = registerPatient.Last_Name,
                Password = "123456",
                Phone_Number = registerPatient.Phone_Number,
                Username = registerPatient.Username,
                IsDefaultPassword = 1,
                Role_ID = 6
            };

            //Hash the password
            newUser.Password = _passwordHasher.HashPassword(newUser, newUser.Password);

            //Save the user to the database
            _appDbContext.Users.Add(newUser);
            await _appDbContext.SaveChangesAsync();

            //Create and save the Patients record
            var patient = new Patients
            {
                User_ID = newUser.User_ID,
                DOB = registerPatient.DOB
            };

            _appDbContext.Patients.Add(patient);
            await _appDbContext.SaveChangesAsync();

            return true;
        }
        public async Task<Users?> GetUserByIdAsync(int userId)
        {
            return await _appDbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.User_ID == userId);
        }
    }
}