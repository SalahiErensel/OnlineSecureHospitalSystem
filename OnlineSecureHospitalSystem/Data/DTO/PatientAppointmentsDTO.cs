namespace OnlineSecureHospitalSystem.Data.DTO
{
    public class PatientAppointmentsDTO
    {
        public int Appointment_ID { get; set; }
        public int? Doctor_ID { get; set; }
        public string? Doctor_First_Name { get; set; }
        public string? Doctor_Last_Name { get; set; }
        public string? Doctor_Specialization { get; set; }
        public DateTime? Appointment_Date { get; set; }
        public string? Appointment_Status{ get; set; }
        public string? Reason { get; set; }
        public string? Extra_Information { get; set; }
    }
}