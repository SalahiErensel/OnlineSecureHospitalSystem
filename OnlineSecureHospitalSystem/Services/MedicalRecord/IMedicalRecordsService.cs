using OnlineSecureHospitalSystem.Data.Models;

namespace OnlineSecureHospitalSystem.Services.MedicalRecord
{
    public interface IMedicalRecordsService
    {
        Task<List<MedicalRecords>> GetOwnMedicalRecordsByUserIdAsync(int userId);
        Task<List<MedicalRecords>> GetMedicalRecordsByPatientIdAsync(int patientId);

    }
}
