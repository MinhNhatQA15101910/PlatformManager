using PlatformService.Entities;

namespace PlatformService.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Platform> Platforms { get; set; }
}
