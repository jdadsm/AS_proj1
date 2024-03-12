using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ASproj1.Server
{
    public static class DatabaseReset
    {
        public static void Reset(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<DbContext>();

            // Drop and recreate database
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Apply migrations
            context.Database.Migrate();
        }
    }
}
