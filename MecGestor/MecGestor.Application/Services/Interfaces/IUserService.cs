using MecGestor.Application.Models.Requests;

namespace MecGestor.Application.Services.Interfaces;

public interface IUserService
{
    Task<Guid> Create(CreateUserRequest request, CancellationToken cancellationToken = default);
}
