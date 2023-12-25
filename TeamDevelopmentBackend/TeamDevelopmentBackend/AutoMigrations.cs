using Microsoft.EntityFrameworkCore;

namespace TeamDevelopmentBackend;


public static class DBConfigurator
{
    public static void MigrateDB<T>(this WebApplication app) where T : DbContext
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbcontext = scope.ServiceProvider.GetRequiredService<T>();
            dbcontext.Database.Migrate();
        }
    }
}