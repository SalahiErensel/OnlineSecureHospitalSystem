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

        // UPDATED: Changed from assignments to appointments-based approach
        public async Task<List<Assignments>> GetAssignmentByDoctorIdAsync(int doctorId)
        {
            return await _appDbContext.Assignments
                .Include(a => a.Patient).ThenInclude(p => p!.User)
                .Include(a => a.CuringDoctor).ThenInclude(d => d!.User)
                .Where(a => a.Curing_Doctor_ID == doctorId)
                .ToListAsync();
        }

        // Get appointments by doctor and status through assignments
        public async Task<List<Appointments>> GetAppointmentsByDoctorAndStatusAsync(int doctorId, string status)
        {
            // For pending appointments, we need to look through assignments since Doctor_ID is not set yet
            if (status == "Pending")
            {
                //Get patient IDs that are assigned to this doctor
                var assignedPatientIds = await _appDbContext.Assignments
                    .Where(a => a.Curing_Doctor_ID == doctorId)
                    .Select(a => a.Patient_ID)
                    .ToListAsync();

                // Get pending appointments for these assigned patients
                return await _appDbContext.Appointments
                    .Where(a => assignedPatientIds.Contains(a.Patient_ID) && a.Appointment_Status == status)
                    .Include(a => a.Patient).ThenInclude(p => p!.User)
                    .OrderBy(a => a.Appointment_Date)
                    .ToListAsync();
            }
            else
            {
                // For scheduled and completed appointments, Doctor_ID should be set
                return await _appDbContext.Appointments
                    .Where(a => a.Doctor_ID == doctorId && a.Appointment_Status == status)
                    .Include(a => a.Patient).ThenInclude(p => p!.User)
                    .OrderBy(a => a.Appointment_Date)
                    .ToListAsync();
            }
        }

        // NEW: Get all appointments for this doctor
        public async Task<List<Appointments>> GetAllAppointmentsByDoctorAsync(int doctorId)
        {
            return await _appDbContext.Appointments
                .Where(a => a.Doctor_ID == doctorId)
                .Include(a => a.Patient).ThenInclude(p => p!.User)
                .OrderByDescending(a => a.Appointment_Date)
                .ToListAsync();
        }

        // NEW: Get patients from completed appointments (replaces assignment-based patients)
        public async Task<List<Patients>> GetPatientsByDoctorAsync(int doctorId)
        {
            var patientIds = await _appDbContext.Appointments
                .Where(a => a.Doctor_ID == doctorId)
                .Select(a => a.Patient_ID)
                .Distinct()
                .ToListAsync();

            return await _appDbContext.Patients
                .Where(p => patientIds.Contains(p.Patient_ID))
                .Include(p => p.User)
                .ToListAsync();
        }

        // NEW: Schedule an appointment (Pending -> Scheduled)
        public async Task<bool> ScheduleAppointmentAsync(int appointmentId, DateTime scheduledDateTime)
        {
            try
            {
                var appointment = await _appDbContext.Appointments.FindAsync(appointmentId);
                if (appointment == null) return false;

                // Find the assignment for this patient to get the doctor ID
                var assignment = await _appDbContext.Assignments
                    .FirstOrDefaultAsync(a => a.Patient_ID == appointment.Patient_ID);

                if (assignment == null) return false;

                // Update the appointment with doctor ID, date/time, and status
                appointment.Doctor_ID = assignment.Curing_Doctor_ID;
                appointment.Appointment_Date = scheduledDateTime;
                appointment.Appointment_Status = "Scheduled";

                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // NEW: Complete an appointment (Scheduled -> Completed)
        public async Task<bool> CompleteAppointmentAsync(int appointmentId)
        {
            try
            {
                var appointment = await _appDbContext.Appointments.FindAsync(appointmentId);
                if (appointment == null) return false;

                appointment.Appointment_Status = "Completed";

                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // NEW: Get appointments for a specific patient with this doctor
        public async Task<List<Appointments>> GetPatientAppointmentsWithDoctorAsync(int patientId, int doctorId)
        {
            return await _appDbContext.Appointments
                .Where(a => a.Patient_ID == patientId && a.Doctor_ID == doctorId && a.Appointment_Status == "Completed")
                .OrderByDescending(a => a.Appointment_Date)
                .ToListAsync();
        }

        public async Task<DateTime?> GetLastAppointmentDateAsync(int patientId, int doctorId)
        {
            return await _appDbContext.Appointments
                .Where(a => a.Patient_ID == patientId && a.Doctor_ID == doctorId)
                .OrderByDescending(a => a.Appointment_Date)
                .Select(a => a.Appointment_Date)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetPendingAppointmentsCountAsync(int doctorId)
        {
            // Get patient IDs assigned to this doctor
            var assignedPatientIds = await _appDbContext.Assignments
                .Where(a => a.Curing_Doctor_ID == doctorId)
                .Select(a => a.Patient_ID)
                .ToListAsync();

            return await _appDbContext.Appointments
                .Where(a => assignedPatientIds.Contains(a.Patient_ID) && a.Appointment_Status == "Pending")
                .CountAsync();
        }

        public async Task<int> GetScheduledAppointmentsCountAsync(int doctorId)
        {
            return await _appDbContext.Appointments
                .Where(a => a.Doctor_ID == doctorId && a.Appointment_Status == "Scheduled")
                .CountAsync();
        }

        public async Task<int> GetCompletedAppointmentsCountAsync(int doctorId)
        {
            return await _appDbContext.Appointments
                .Where(a => a.Doctor_ID == doctorId && a.Appointment_Status == "Completed")
                .CountAsync();
        }

        public async Task<int> GetPatientsCountAsync(int doctorId)
        {
            // Get unique patient IDs from both assignments (pending) and appointments (scheduled/completed)
            var assignedPatientIds = await _appDbContext.Assignments
                .Where(a => a.Curing_Doctor_ID == doctorId)
                .Select(a => a.Patient_ID)
                .ToListAsync();

            var appointmentPatientIds = await _appDbContext.Appointments
                .Where(a => a.Doctor_ID == doctorId)
                .Select(a => a.Patient_ID)
                .ToListAsync();

            var allPatientIds = assignedPatientIds.Union(appointmentPatientIds).Distinct();
            return allPatientIds.Count();
        }

        public async Task<int> GetMedicalRecordsCountAsync(int doctorId)
        {
            return await _appDbContext.MedicalRecords
                .Where(mr => mr.Curing_Doctor_ID == doctorId)
                .CountAsync();
        }

        public async Task<List<Doctors>> GetConsultingDoctorsAsync()
        {
            return await _appDbContext.Doctors
              .Include(u => u.User)
              .Where(d => d.User!.Role_ID == 5)
              .ToListAsync();
        }

        public async Task<bool> AssignConsultingDoctorAsync(int patientId, int assignerCuringDoctorId, int consultingDoctorId)
        {
            try
            {
                var assignment = new Assignments
                {
                    Patient_ID = patientId,
                    Curing_Doctor_ID = consultingDoctorId,
                    Assigned_By = assignerCuringDoctorId
                };
                _appDbContext.Assignments.Add(assignment);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        } 
    }
}