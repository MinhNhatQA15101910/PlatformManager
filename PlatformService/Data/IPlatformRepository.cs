using PlatformService.Entities;

namespace PlatformService.Data;

public interface IPlatformRepository
{
    void CreatePlatform(Platform platform);
    Task<IEnumerable<Platform>> GetAllPlatformsAsync();
    Task<Platform?> GetPlatformByIdAsync(int id);
    Task<bool> SaveChangesAsync();
}
