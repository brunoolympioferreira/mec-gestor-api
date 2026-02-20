namespace MecGestor.Application.Models.Requests;

public record LoginRequest(string Email, string Password)
{
    public Domain.Entities.User ToEntity() => new(Email, Password);
}
