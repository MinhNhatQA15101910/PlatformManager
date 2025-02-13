using PlatformService.Entities;

namespace PlatformService.Data;

public class PlatformRepository(DataContext context) : IPlatformRepository
{
    public void CreatePlatform(Platform platform)
    {
        context.Platforms.Add(platform);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return [.. context.Platforms];
    }

    public Platform? GetPlatformById(int id)
    {
        return context.Platforms.Find(id);
    }

    public bool SaveChanges()
    {
        return context.SaveChanges() > 0;
    }
}
