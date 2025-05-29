namespace OnlineSecureHospitalSystem.Data.DTO
{
    public class AuthenticatedUser
    {
        public int User_ID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public int IsDefaultPassword { get; set; }
    }
}