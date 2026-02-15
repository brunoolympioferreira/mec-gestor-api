using MecGestor.Application.EventHandlers;
using MecGestor.Application.Services.Authentication;
using MecGestor.Application.Services.Company;
using MecGestor.Application.Services.Interfaces;
using MecGestor.Application.Services.User;
using MecGestor.Domain.Events;
using MecGestor.Domain.Intefaces.Events;
using Microsoft.Extensions.DependencyInjection;

namespace MecGestor.Application;

public static class ApplicationModule
{
    public static void AddApplicationModule(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddEventHandler();
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<ICompanyService, CompanyService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IAuthService, AuthService>();

        return services;
    }

    public static IServiceCollection AddEventHandler(this IServiceCollection services)
    {
        return services.AddScoped<IEventHandler<CompanyCreatedEvent>, CreateAdminUserWhenCompanyCreatedHandler>();
    }
}
