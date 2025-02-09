using PlatformService.Data;

namespace PlatformService.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddControllers();
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseInMemoryDatabase("PlatformDB");
        });
        services.AddScoped<IPlatformRepository, PlatformRepository>();

        return services;
    }
}
