using MecGestor.Domain.Entities;

namespace MecGestor.Application.Models.Requests;

public record CreateUserRequest(string Username, string Email, string Password, string Role, bool Active, Guid CompanyId)
{
    public User ToEntity()
    {
        return new User(Username, Email, Password, Role, Active, CompanyId);
    }
}
