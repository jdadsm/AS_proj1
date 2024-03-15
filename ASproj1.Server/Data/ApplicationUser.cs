using Microsoft.AspNetCore.Identity;

namespace ASproj1.Server.Data
{
    public class ApplicationUser : IdentityUser
    {
        public Patient Patient { get; set; }
        public ApplicationUser() { }
        public ApplicationUser(string id, string email, string password)
        {
            this.Id = id;
            Email = email;
            NormalizedEmail = email.ToUpper();
            UserName = email;
            NormalizedUserName = email.ToUpper();
            PhoneNumber = String.Empty;
            LockoutEnabled = true;
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            PasswordHash = passwordHasher.HashPassword(this, password);
        }
    }
}
