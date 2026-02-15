using MecGestor.Application.Models.Requests;
using MecGestor.Application.Services.Interfaces;
using MecGestor.Domain.Events;
using MecGestor.Domain.Intefaces.Events;

namespace MecGestor.Application.EventHandlers;

/// <summary>
/// Handler responsável por criar o usuário administrador padrão quando uma Company é criada
/// </summary>
public class CreateAdminUserWhenCompanyCreatedHandler(
    IUserService userService) : IEventHandler<CompanyCreatedEvent>
{
    private const string DefaultAdminUsername = "admin";
    private const string DefaultAdminPassword = "admin";

    public async Task HandleAsync(CompanyCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        try
        {
            var createUserRequest = new CreateUserRequest($"{DefaultAdminUsername}", @event.CompanyEmail, DefaultAdminPassword, "Administrator", true, @event.CompanyId);

            var userId = await userService.Create(createUserRequest, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }
}
