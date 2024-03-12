using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using static ASproj1.Server.Controllers.RecordsController;

namespace ASproj1.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Patient>()
                .HasOne(p => p.MedicalRecord)
                .WithOne(m => m.Patient)
                .HasForeignKey<MedicalRecord>(m => m.MedicalRecordNumber);

            builder.Entity<Patient>()
                .HasOne(p => p.User)
                .WithOne(m => m.Patient)
                .HasForeignKey<ApplicationUser>(u => u.Id);

            base.OnModelCreating(builder);
            
        }

    }
}
