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
        [Required(ErrorMessage = "Firstname is required")]
        public string? First_Name { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string? Last_Name { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Invalid phone number")]
        public string? Phone_Number { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }
        public int? IsDefaultPassword { get; set; }
        [ForeignKey(nameof(Role))]
        public int Role_ID { get; set; }
        public Roles? Role { get; set; }
    }
}