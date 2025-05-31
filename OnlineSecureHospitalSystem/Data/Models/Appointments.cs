using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSecureHospitalSystem.Data.Models
{
    public class Appointments
    {
        [Key]
        public int Appointment_ID { get; set; }
        [ForeignKey(nameof(Patient))]
        public int Patient_ID { get; set; }
        [ForeignKey(nameof(Doctor))]
        public int? Doctor_ID { get; set; }
        public DateTime? Appointment_Date { get; set; }
        public string? Appointment_Status { get; set; }
        public string? Reason { get; set; }
        public string? Extra_Information { get; set; }
        public Patients? Patient {  get; set; }
        public Doctors? Doctor { get; set; }
    }
}