using PlatformService.Entities;

namespace PlatformService.Data;

public class PlatformRepository(DataContext context) : IPlatformRepository
{
    public void CreatePlatform(Platform platform)
    {
        context.Platforms.Add(platform);
    }

    public async Task<IEnumerable<Platform>> GetAllPlatformsAsync()
    {
        return await context.Platforms.ToListAsync();
    }

    public async Task<Platform?> GetPlatformByIdAsync(int id)
    {
        return await context.Platforms.FindAsync(id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
