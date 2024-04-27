using Microsoft.EntityFrameworkCore;
using Minimal_API.Persistance;

namespace Minimal_API.API.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using AgencyDbContext dbContext = scope.ServiceProvider.GetRequiredService<AgencyDbContext>();
        
        dbContext.Database.Migrate();
    }
}