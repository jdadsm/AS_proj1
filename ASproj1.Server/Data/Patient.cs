using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASproj1.Server.Data
{
    public class Patient
    {
        [Key]
        public string PatientId { get; set; }
        public string? MedicalRecordId { get; set; }
        [ForeignKey("MedicalRecordId")]
        public MedicalRecord? MedicalRecord { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
        public string AccessCode { get; set; }
        public string Role {  get; set; }
        public Patient(string accessCode)
        {
            this.PatientId = Guid.NewGuid().ToString();
            this.Role = "DefaultUser";
            this.AccessCode = accessCode;
        }

        public Patient(string medicalRecordId, string userId, string accessCode)
        {
            this.PatientId = Guid.NewGuid().ToString();
            this.MedicalRecordId = medicalRecordId;
            this.UserId = userId;
            this.AccessCode = accessCode;

            this.Role = "DefaultUser";
        }


    }
}
