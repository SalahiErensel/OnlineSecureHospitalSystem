using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSecureHospitalSystem.Data.Models
{
    public class Users
    {
        [Key]
        public int User_ID { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }    
        public string? Password { get; set; } 
        public int? IsDefaultPassword { get; set; }

        [ForeignKey(nameof(Role))]
        public int Role_ID { get; set; }
        public Roles? Role { get; set; }
    }
}
