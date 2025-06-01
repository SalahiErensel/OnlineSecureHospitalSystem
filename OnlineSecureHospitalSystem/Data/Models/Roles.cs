using System.ComponentModel.DataAnnotations;

namespace OnlineSecureHospitalSystem.Data.Models
{
    public class Roles
    {
        [Key]
        public int Role_ID { get; set; }
        public string? Role_Name { get; set; }
    }
}