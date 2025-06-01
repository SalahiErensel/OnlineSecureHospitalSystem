using OnlineSecureHospitalSystem.Data.Models;

namespace OnlineSecureHospitalSystem.Services.MedicalRecord
{
    public interface IMedicalRecordsService
    {
        Task<List<MedicalRecords>> GetOwnMedicalRecordsByUserIdAsync(int userId);
        Task<List<MedicalRecords>> GetMedicalRecordsByPatientIdAsync(int patientId);
        Task<bool> HasMedicalRecordForAppointmentAsync(int appointmentId);
        Task<MedicalRecords?> GetMedicalRecordByAppointmentIdAsync(int appointmentId);
        Task<List<MedicalRecords>> GetMedicalRecordsByDoctorAsync(int doctorId);
        Task<bool> CreateMedicalRecordAsync(MedicalRecords medicalRecord);
        Task<bool> UpdateMedicalRecordAsync(int appointmentId, int doctorId, byte[] encryptedData, string signature, bool isSensitive);
        Task<int> GetMedicalRecordsCountByDoctorAsync(int doctorId);
    }
}