using Microsoft.Extensions.DependencyInjection;

namespace MecGestor.Infra;

public static class InfraModule
{
    public static void AddInfraModule(this IServiceCollection services, string connectionString)
    {
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services;
    }

    private static IServiceCollection AddUnityOfWork(this IServiceCollection services)
    {
        return services;
    }
}
