namespace MecGestor.Application.Services.Interfaces;

public interface IAuthService
{
    string GenerateJwtToken(Domain.Entities.User user);
}
