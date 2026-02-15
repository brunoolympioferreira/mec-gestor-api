using MecGestor.Domain.Intefaces.Contracts;
using MecGestor.Domain.Intefaces.Events;
using MecGestor.Domain.Intefaces.Repositories;
using MecGestor.Infra.Events;
using MecGestor.Infra.Persistence;
using MecGestor.Infra.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MecGestor.Infra;

public static class InfraModule
{
    public static void AddInfraModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DatabaseConnection") ?? throw new ArgumentNullException("ConnectionString Requerida");
        services
            .AddDatabase(connectionString)
            .AddUnityOfWork()
            .AddRepositories()
            .AddEventDispatcher();
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<MecGestorDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        return services;
    }

    private static IServiceCollection AddUnityOfWork(this IServiceCollection services)
    {
        return services.AddScoped<IUnityOfWork, UnityOfWork>();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<ICompanyRepositroy, CompanyRepository>()
            .AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    private static IServiceCollection AddEventDispatcher(this IServiceCollection services)
    {
        return services.AddScoped<IEventDispatcher, EventDispatcher>();
    }
}
