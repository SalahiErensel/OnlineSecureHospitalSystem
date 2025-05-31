using Microsoft.EntityFrameworkCore;
using OnlineSecureHospitalSystem.Data;
using OnlineSecureHospitalSystem.Data.Models;

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
            return await _appDbContext.Patients.Include(u => u.User).ToListAsync();
        }

        public async Task<int> GetTotalPendingAssignmentsAsync()
        {
            return await _appDbContext.Appointments.Where(x => x.Appointment_Status == "Pending")
                .CountAsync();
        }
        public async Task<int> GetTotalPatientsAsync()
        {
            return await _appDbContext.Patients.CountAsync();
        }

        public async Task<int> GetTotalDoctorsAsync()
        {
            return await _appDbContext.Doctors.Where(x => x.User!.Role_ID == 4).CountAsync();
        }

        public async Task<List<Appointments>> GetPendingAppointmentsAsync()
        {
            return await _appDbContext.Appointments
                .Where(a => a.Appointment_Status == "Pending")
                .Include(a => a.Patient).ThenInclude(p => p!.User)
                .Include(a => a.Doctor).ThenInclude(d => d!.User)
                .ToListAsync();
        }

        public async Task<List<Doctors>> GetToBeAssignedDoctorsAsync()
        {
            return await _appDbContext.Doctors.Include(u => u.User).Where(x => x.User!.Role_ID == 4).ToListAsync();
        }

        public async Task<bool> AssignCuringDoctorAsync(Assignments assignment)
        {
            try
            {
                _appDbContext.Assignments.Add(assignment);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Doctors?> GetDoctorByUserIdAsync(int userId)
        {
            return await _appDbContext.Doctors.Include(u => u.User).FirstOrDefaultAsync(d => d.User_ID == userId);
        }

        
    }
}
