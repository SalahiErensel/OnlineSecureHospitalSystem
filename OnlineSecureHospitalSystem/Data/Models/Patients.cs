using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSecureHospitalSystem.Data.Models
{
    public class Patients
    {
        [Key]
        public int Patient_ID { get; set; }
        [ForeignKey(nameof(User))]
        public int User_ID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateOnly DOB { get; set; }
        public string? Gender { get; set; }
        public string? Phone_Number { get; set; }
        public Users? User { get; set; }
    }
}