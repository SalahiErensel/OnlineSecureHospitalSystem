using Microsoft.EntityFrameworkCore;
using OnlineSecureHospitalSystem.Data;
using OnlineSecureHospitalSystem.Data.Models;
using System.Collections.Generic;

namespace OnlineSecureHospitalSystem.Services.Doctor
{
    public class DoctorService : IDoctorService
    {
        private readonly AppDbContext _appDbContext;

        public DoctorService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Patients>> GetAllPatientsAsync()
        {
            return await _appDbContext.Patients.Include(u=> u.User).ToListAsync();
        }
    }
}
