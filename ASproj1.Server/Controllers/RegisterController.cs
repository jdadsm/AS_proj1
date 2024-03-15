using Microsoft.AspNetCore.Mvc;
using ASproj1.Server.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Security.Cryptography;

namespace ASproj1.Server.Controllers
{
    [ApiController]
    [Route("api/register")]
    public class RegisterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class RegisterModel
        {
            [Required]
            public required string Email { get; set; }
            [Required]
            public required string Password { get; set; }
            [Required]
            public required string AccessCode { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.ApplicationUsers.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email is already in use.");
                return BadRequest(ModelState);
            }

            

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Patient patient = new Patient(BitConverter.ToString(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(model.AccessCode))).Replace("-", ""));
                    MedicalRecord medicalRecord = new MedicalRecord(patient.PatientId);
                    ApplicationUser user = new ApplicationUser(patient.PatientId, model.Email, model.Password); 
                    patient.User = user;
                    patient.MedicalRecord = medicalRecord;

                    _context.Patients.Add(patient);
                    _context.MedicalRecords.Add(medicalRecord);
                    _context.ApplicationUsers.Add(user);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");

                    await transaction.RollbackAsync();

                    return BadRequest(ex);
                }
            }
            return Ok();
        }
    }
}
