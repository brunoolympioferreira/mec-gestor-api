namespace MecGestor.Application.Models.Requests;

public record LoginRequest(string Email, string Password, Guid CompanyId)
{
    public Domain.Entities.User ToEntity() => new(Email, Password, CompanyId);
}
