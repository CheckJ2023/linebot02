using Microsoft.EntityFrameworkCore;
namespace linebot02.Startup;
public static class DependencyInjectionSetup
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration Configuration)
    {

        // services.AddTransient<MyDbContext>(); 
        services.AddDbContext<MyDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection"))));

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddAuthentication();

        return services;
    }
}