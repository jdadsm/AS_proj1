using ASproj1.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

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
                    command.CommandText = "GetRecords";
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
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
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
        public async Task<IActionResult> Patch([FromBody] RecordsModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            
            
            var result = await _context.Database
                .ExecuteSqlRawAsync("EXEC UpdateRecords @UserId, @PhoneNumber, @TreatmentPlan, @DiagnosisDetails, @FullName",
                    new SqlParameter("@UserId", userId),
                    new SqlParameter("@PhoneNumber", model.PhoneNumber),
                    new SqlParameter("@TreatmentPlan", model.TreatmentPlan),
                    new SqlParameter("@DiagnosisDetails", model.DiagnosisDetails),
                    new SqlParameter("@FullName", model.FullName));

            if (result == 200)
            {
                return Ok();
            }
            else if (result == 404)
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
