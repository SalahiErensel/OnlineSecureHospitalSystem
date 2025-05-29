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
        public string? Specialization { get; set; }
        public int Is_Chief { get; set; }
        public Users? User { get; set; }
    }
}