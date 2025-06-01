using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineSecureHospitalSystem.Data;
using OnlineSecureHospitalSystem.Data.Models;

namespace OnlineSecureHospitalSystem.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHasher<Users> _passwordHasher;

        public AdminService(AppDbContext appDbContext, IPasswordHasher<Users> passwordHasher)
        {
            _appDbContext = appDbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _appDbContext.Users.Include(u => u.Role).ToListAsync();
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
                Password = "123456", //Default password
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
                //If the user is a chief doctor, set Is_Chief to 1, otherwise 0
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
                //If the user is a patient, set the date of birth
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
    }
}