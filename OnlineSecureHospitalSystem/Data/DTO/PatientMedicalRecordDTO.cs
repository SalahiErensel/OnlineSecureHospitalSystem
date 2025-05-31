namespace OnlineSecureHospitalSystem.Data.DTO
{
    public class PatientMedicalRecordDTO
    {
        public string? Doctor_Name { get; set; }
        public string? Doctor_Lastname { get; set; }
        public string? Doctor_Specialization { get; set; }
        public string? Patient_Complaint { get; set; }
        public string? Result { get; set; }
        public bool? Is_Sensitive { get; set; }
        public DateTime? Appointment_Date { get; set; }
    }
}
