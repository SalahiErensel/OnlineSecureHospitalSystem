using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSecureHospitalSystem.Data.Models
{
    public class Assignments
    {
        [Key]
        public int Assignment_ID { get; set; }
        [ForeignKey(nameof(Patient))]
        public int Patient_ID { get; set; }
        [ForeignKey(nameof(CuringDoctor))]
        public int Curing_Doctor_ID { get; set; }
        [ForeignKey(nameof(AssignedByDoctor))]
        public int Assigned_By { get; set; }
        public Patients? Patient { get; set; }
        public Doctors? CuringDoctor { get; set; }
        public Doctors? AssignedByDoctor { get; set; }
    }
}