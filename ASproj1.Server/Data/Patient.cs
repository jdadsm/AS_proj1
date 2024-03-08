using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ASproj1.Server.Data
{
    public class Patient
    {
        [Key]
        public string PatientId { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public IdentityUser User { get; set; }
        public string AccessCode { get; set; }
        public Patient()
        {
            this.PatientId = Guid.NewGuid().ToString();
            this.MedicalRecord = new MedicalRecord();
            MedicalRecord.MedicalRecordNumber = this.PatientId;
            this.User = new IdentityUser
            {
                Id = this.PatientId
            };
            this.AccessCode = string.Empty;
        }
        public Patient(string email, string password)
        {
            this.PatientId = Guid.NewGuid().ToString();
            this.MedicalRecord = new MedicalRecord();
            MedicalRecord.MedicalRecordNumber = this.PatientId;
            this.User = new IdentityUser
            {
                Email = email,
                PasswordHash = password,
                Id = this.PatientId
            };
            this.AccessCode = string.Empty;
        }
         

    }
}
