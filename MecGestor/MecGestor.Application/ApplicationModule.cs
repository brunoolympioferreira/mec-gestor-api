using MecGestor.Application.Services.Company;
using MecGestor.Application.Services.Interfaces;
using MecGestor.Application.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace MecGestor.Application;

public static class ApplicationModule
{
    public static void AddApplicationModule(this IServiceCollection services)
    {
        services.AddServices();
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<ICompanyService, CompanyService>()
            .AddScoped<IUserService, UserService>();

        return services;
    }
}
