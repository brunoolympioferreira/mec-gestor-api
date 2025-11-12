using Microsoft.Extensions.DependencyInjection;

namespace MecGestor.Application;

public static class ApplicationModule
{
    public static void AddApplicationModule(this IServiceCollection services)
    {
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }
}
