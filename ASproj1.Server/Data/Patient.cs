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
        public ApplicationUser User { get; set; }
        public string AccessCode { get; set; }
        public Patient()
        {
            this.PatientId = Guid.NewGuid().ToString();
            this.MedicalRecord = new MedicalRecord(this.PatientId);
            this.User = new ApplicationUser
            {
                Id = this.PatientId
            };
            this.AccessCode = string.Empty;
        }
        public Patient(string email, string password)
        {
            this.PatientId = Guid.NewGuid().ToString();
            this.MedicalRecord = new MedicalRecord(this.PatientId);
            this.User = new ApplicationUser
            {
                UserName = email,
                NormalizedUserName = email.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                Id = this.PatientId,
                LockoutEnabled = true
            };
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            User.PasswordHash = passwordHasher.HashPassword(this.User, password);
            this.AccessCode = string.Empty;
        }
         

    }
}
