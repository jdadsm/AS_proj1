using ASproj1.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace ASproj1.Server.Controllers
{
    [ApiController]
    [Route("/api/records")]
    public class RecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class RecordsModel
        {
            public required string FullName { get; set; }
            public required string PhoneNumber { get; set; }
            public required string DiagnosisDetails {  get; set; }
            public required string TreatmentPlan { get; set; }

        }

        public class UpdateRecordsModel
        {
            public required string Email { get; set; }
            public required string AccessCode { get; set; }
            public required string FullName { get; set; }
            public required string PhoneNumber { get; set; }
            public required string DiagnosisDetails { get; set; }
            public required string TreatmentPlan { get; set; }
        }

        public class InputRecordsModel
        {
            public required string Email { get; set; }
            public required string AccessCode { get; set; }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            var userIdParameter = new SqlParameter("@UserId", userId);

            RecordsModel result = null;

            using (var connection = _context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetRecordsSelf";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(userIdParameter);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            result = new RecordsModel
                            {
                                // Map properties from reader to RecordsModel object
                                FullName = reader.GetString(reader.GetOrdinal("UserName")),
                                PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? "" : reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                DiagnosisDetails = reader.GetString(reader.GetOrdinal("DiagnosisDetails")),
                                TreatmentPlan = reader.GetString(reader.GetOrdinal("TreatmentPlan"))
                            };
                        }
                    }
                }
            }

            if (result == null)
            {
                return NotFound("Record not found.");
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] InputRecordsModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var targetUser = _context.ApplicationUsers.First(u => u.Email == model.Email);

            if (targetUser == null)
            {
                return NotFound();
            }

            var userIdParameter = new SqlParameter("@UserId", targetUser.Id);

            var patient = _context.Patients.First(p => p.PatientId == targetUser.Id);

            if(patient == null)
            {
                return NotFound();
            }
            var executeAs = "HelpDesk";
            
            var hashedAccessCode = BitConverter.ToString(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(model.AccessCode))).Replace("-", "");
            if (hashedAccessCode==patient.AccessCode)
            {
                executeAs = "DefaultUser";
            }

            var executeAsParameter = new SqlParameter("@ExecuteAs", executeAs);

            RecordsModel result = null;

            using (var connection = _context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetRecords";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(userIdParameter);
                    command.Parameters.Add(executeAsParameter);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            result = new RecordsModel
                            {
                                // Map properties from reader to RecordsModel object
                                FullName = reader.GetString(reader.GetOrdinal("UserName")),
                                PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? "" : reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                DiagnosisDetails = reader.GetString(reader.GetOrdinal("DiagnosisDetails")),
                                TreatmentPlan = reader.GetString(reader.GetOrdinal("TreatmentPlan"))
                            };
                        }
                    }
                }
            }

            if (result == null)
            {
                return NotFound("Record not found.");
            }

            return Ok(result);
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> Patch([FromBody] UpdateRecordsModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.Email == string.Empty)
            {
                var userId = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");


                var result = await _context.Database
                    .ExecuteSqlRawAsync("EXEC UpdateRecordsSelf @UserId, @PhoneNumber, @TreatmentPlan, @DiagnosisDetails, @FullName",
                        new SqlParameter("@UserId", userId),
                        new SqlParameter("@PhoneNumber", model.PhoneNumber),
                        new SqlParameter("@TreatmentPlan", model.TreatmentPlan),
                        new SqlParameter("@DiagnosisDetails", model.DiagnosisDetails),
                        new SqlParameter("@FullName", model.FullName));

                if (result > 0)
                {
                    return Ok();
                }
                else if (result == 0)
                {
                    return NotFound("Patient not found.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the patient record.");
                }
            }
            else
            {
                var targetUser = _context.ApplicationUsers.First(u => u.Email == model.Email);

                if (targetUser == null)
                {
                    return NotFound();
                }

                var patient = _context.Patients.First(p => p.PatientId == targetUser.Id);

                if (patient == null)
                {
                    return NotFound();
                }

                var result = -1;

                var executeAs = "HelpDesk";
                var hashedAccessCode = BitConverter.ToString(new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(model.AccessCode))).Replace("-", "");
                if (hashedAccessCode == patient.AccessCode)
                {
                    executeAs = "DefaultUser";
                    result = await _context.Database
                    .ExecuteSqlRawAsync("EXEC UpdateRecords @UserId, @ExecuteAs, @PhoneNumber, @TreatmentPlan, @DiagnosisDetails, @FullName",
                        new SqlParameter("@UserId", targetUser.Id),
                        new SqlParameter("@ExecuteAs", executeAs),
                        new SqlParameter("@PhoneNumber", model.PhoneNumber),
                        new SqlParameter("@TreatmentPlan", model.TreatmentPlan),
                        new SqlParameter("@DiagnosisDetails", model.DiagnosisDetails),
                        new SqlParameter("@FullName", model.FullName));
                }
                else
                {
                    result = await _context.Database
                    .ExecuteSqlRawAsync("EXEC UpdateRecordsNoAccess @UserId, @ExecuteAs, @FullName",
                        new SqlParameter("@UserId", targetUser.Id),
                        new SqlParameter("@ExecuteAs", executeAs),
                        new SqlParameter("@FullName", model.FullName));
                }
                

                if (result > 0)
                {
                    return Ok();
                }
                else if (result == 0)
                {
                    return NotFound("Patient not found.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the patient record.");
                }
            }
            

            

        }
    }
}
