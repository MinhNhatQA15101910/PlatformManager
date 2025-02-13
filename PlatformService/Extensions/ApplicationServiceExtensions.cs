using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

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
        services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
