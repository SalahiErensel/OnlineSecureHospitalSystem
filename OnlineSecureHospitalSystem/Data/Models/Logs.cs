namespace OnlineSecureHospitalSystem.Data.Models
{
    public class Logs
    {
        public int Id { get; set; }
        public string Action { get; set; } = string.Empty;
        public byte[]? InputParameters { get; set; }
        public byte[]? OutputParameters { get; set; }
        public DateTime Timestamp { get; set; }
    }
}