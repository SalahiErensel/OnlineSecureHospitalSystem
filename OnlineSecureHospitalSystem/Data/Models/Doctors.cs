using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSecureHospitalSystem.Data.Models
{
    public class Doctors
    {
        [Key]
        public int Doctor_ID { get; set; }
        [ForeignKey(nameof(User))]
        public int User_ID { get; set; }
        public int Is_Chief { get; set; }
        public string? Expertise { get; set; }
        public Users? User { get; set; }
    }
}