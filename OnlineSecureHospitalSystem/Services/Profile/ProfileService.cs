using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineSecureHospitalSystem.Data;
using OnlineSecureHospitalSystem.Data.DTO;
using OnlineSecureHospitalSystem.Data.Models;

namespace OnlineSecureHospitalSystem.Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly AppDbContext _appDbContext;

        public ProfileService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<DateTime> GetPatientDOBAsync(int userId)
        {
            var patient = await _appDbContext.Patients.FirstOrDefaultAsync(x=> x.User_ID == userId);

            if (patient == null)
            {
                throw new Exception("Patient not found");
            }
            return patient.DOB!.Value;
        }

        public async Task<string> GetDoctorSpecializationAsync(int userId)
        {
            var doctor = await _appDbContext.Doctors.FirstOrDefaultAsync(x => x.User_ID == userId);

            if (doctor == null)
            {
                throw new Exception("Doctor not found");
            }
            return doctor.Specialization ?? "Not specified";
        }

        public async Task<bool> UpdateProfileAsync(UpdateProfileDTO updateProfileDto)
        {
           //update the profile of user
            var user = await _appDbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.User_ID == updateProfileDto.User_ID);
            
            user!.First_Name = updateProfileDto.First_Name;
            user.Last_Name = updateProfileDto.Last_Name;
            user.Email = updateProfileDto.Email;
            user.Phone_Number = updateProfileDto.Phone_Number;
            user.Address = updateProfileDto.Address;
            if(user.Password != null)
            {
                //Hash the passwordif it is provided
                var passwordHasher = new PasswordHasher<Users>();

                user.Password = passwordHasher.HashPassword(user, updateProfileDto.Password!);
            }
            if (user!.Role!.Role_ID == 3 || user!.Role!.Role_ID == 4 || user!.Role!.Role_ID == 5)
            {
                var doctor = await _appDbContext.Doctors.FirstOrDefaultAsync(x => x.User_ID == updateProfileDto.User_ID);
                if (doctor != null)
                {
                    doctor.Specialization = updateProfileDto.Specialization;
                }
            }
            
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}