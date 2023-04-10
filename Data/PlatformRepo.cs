using PlatformService.Models;

namespace PlatformService.Data;

public class PlatformRepo : IPlatformRepo
{
    private readonly AppDbContext dbContext;

    public PlatformRepo(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public void CreatePlatform(Platform? platform)
    {
        if (platform == null)
            throw new ArgumentNullException(nameof(platform));

        this.dbContext.Add(platform);
    }

    public IEnumerable<Platform> GetAllPlatforms() =>
        this.dbContext.Platforms.ToList();

    public Platform? GetPlatformById(int id) =>
        this.dbContext.Platforms.FirstOrDefault(p => p.Id == id);

    public bool SaveChanges() =>
        this.dbContext.SaveChanges() >= 0;
}