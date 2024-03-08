using Microsoft.AspNetCore.Mvc;
using ASproj1.Server.Data;

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
            public required string Email { get; set; }
            public required string Password { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Patient patient = new Patient(model.Email,model.Password);

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
