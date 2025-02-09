using PlatformService.Entities;

namespace PlatformService.Data;

public class Seed
{
    public static async Task SeedPlatforms(DataContext context)
    {
        if (await context.Platforms.AnyAsync()) return;

        var platformData = await File.ReadAllTextAsync("Data/PlatformSeedData.json");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var platforms = JsonSerializer.Deserialize<List<Platform>>(platformData, options);

        if (platforms == null) return;

        foreach (var platform in platforms)
        {
            context.Platforms.Add(platform);
        }

        await context.SaveChangesAsync();
    }
}
