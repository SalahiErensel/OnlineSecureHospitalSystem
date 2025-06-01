namespace OnlineSecureHospitalSystem.Data.DTO
{
    public class UpdateProfileDTO
    {
        public int User_ID { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? Email { get; set; }
        public string? Phone_Number { get; set; }
        public string? Specialization { get; set; } 
        public string? Address { get; set; }
        public string? Password { get; set; } 
    }
}