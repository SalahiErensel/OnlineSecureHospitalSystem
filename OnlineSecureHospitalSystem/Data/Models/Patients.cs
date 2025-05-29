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
        public DateTime? DOB { get; set; }
        public Users? User { get; set; }
    }
}