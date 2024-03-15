using Microsoft.AspNetCore.Mvc;
using ASproj1.Server.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ASproj1.Server.Controllers
{
    [ApiController]
    [Route("/api/role")]
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            var patient = _context.Patients.First(p => p.PatientId == userId);
            
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(new { role = patient.Role });
        }
    }
}
