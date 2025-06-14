﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSecureHospitalSystem.Data.Models
{
    public class MedicalRecords
    {
        [Key]
        public int Record_ID { get; set; }
        [ForeignKey(nameof(Doctor))]
        public int Curing_Doctor_ID { get; set; }
        [ForeignKey(nameof(Patient))]
        public int Patient_ID { get; set; }
        [ForeignKey(nameof(Appointment))]
        public int Appointment_ID { get; set; }
        public byte[]? Record_Data { get; set; }
        public string? Signature { get; set; }
        public bool Is_Sensitive { get; set; }
        public Doctors? Doctor { get; set; }
        public Patients? Patient { get; set; }
        public Appointments? Appointment { get; set; }
    }
}