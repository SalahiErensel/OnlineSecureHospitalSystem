using OnlineSecureHospitalSystem.Data.Models;

namespace OnlineSecureHospitalSystem.Data.DTO
{
    public class ConsultingPatientInfoDTO
    {
        public int Patient_ID { get; set; }
        public int Assigned_By { get; set; }
        public Patients? Patient { get; set; }
    }
}