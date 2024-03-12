using Microsoft.AspNetCore.Identity;

namespace ASproj1.Server.Data
{
    public class ApplicationUser : IdentityUser
    {
        public Patient Patient { get; set; }
    }
}
