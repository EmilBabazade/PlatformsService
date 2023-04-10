using PlatformService.Models;

namespace PlatformService.Data;

public static class PrepDb
{
    public static void PopulateDb(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
    }

    private static void SeedData(AppDbContext? dbContext)
    {
        if (dbContext == null)
            throw new ArgumentNullException(nameof(dbContext));

        if (dbContext.Platforms.Any())
        {
            Console.WriteLine("--> We already have data, no seeding required");
            return;
        }

        Console.WriteLine("--> Seeding Data...");
        dbContext.Platforms.AddRange(
            new Platform
            {
                Name = "Dot Net",
                Publisher = "Microsoft",
                Cost = "Free",
            },
            new Platform
            {
                Name = "SQL Server Express",
                Publisher = "Microsoft",
                Cost = "Free",
            },
            new Platform
            {
                Name = "Kubernetes",
                Publisher = "Cloud Native Computing Foundation",
                Cost = "Free",
            }
        );
        dbContext.SaveChanges();
    }
}