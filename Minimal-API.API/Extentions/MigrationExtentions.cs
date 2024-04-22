using Microsoft.EntityFrameworkCore;
using Minimal_API.Persistance;

namespace Minimal_API.API.Extentions;

public static class MigrationExtentions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using AgencyDbContext dbContext = scope.ServiceProvider.GetRequiredService<AgencyDbContext>();
        
        dbContext.Database.Migrate();
    }
}