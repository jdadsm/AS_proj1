
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASproj1.Server.Data;
using System.Security.Claims;

namespace ASproj1.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddAuthorization();
            builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            ExecuteInitializationScripts();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.MapIdentityApi<ApplicationUser>();

            app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) =>
            {

                await signInManager.SignOutAsync();
                return Results.Ok();

            }).RequireAuthorization();


            app.MapGet("/pingauth", (ClaimsPrincipal user) =>
            {
                var email = user.FindFirstValue(ClaimTypes.Email); // get the user's email from the claim
                return Results.Json(new { Email = email }); ; // return the email as a plain text response
            }).RequireAuthorization();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }

        private static void ExecuteInitializationScripts()
        {
            try
            {
                var script1 = File.ReadAllText("SQLScripts/CreateRoles.sql");
                var script2 = File.ReadAllText("SQLScripts/DefineMaskedColumns.sql");
                var script3 = File.ReadAllText("SQLScripts/GetRecords.sql");
                var script4 = File.ReadAllText("SQLScripts/GetRecordsSelf.sql");
                var script5 = File.ReadAllText("SQLScripts/GetRole.sql");
                var script6 = File.ReadAllText("SQLScripts/UpdateRecords.sql");
                var script7 = File.ReadAllText("SQLScripts/UpdateRecordsSelf.sql");
                var script8 = File.ReadAllText("SQLScripts/UpdateRecordsNoAccess.sql");

            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error executing initialization scripts: {ex.Message}");
            }
        }
    }
}