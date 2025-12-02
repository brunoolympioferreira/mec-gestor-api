using MecGestor.Domain.Extensions;
using MecGestor.Domain.ValueObjects;

namespace MecGestor.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; private set; }
    public Email Email { get; private set; }
    public string PasswordHash { get; private set; }
    public Role Role { get; private set; }
    public bool Active { get; private set; }

    /// <summary>
    /// Associação com tabela Company
    /// </summary>
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; }

    public User(string username, string email, string password, string role, bool active, Guid companyId)
    {
        Username = username;
        Email = Email.Create(email);
        PasswordHash = password.HashPassword();
        Role = Role.Create(role);
        Active = active;
        CompanyId = companyId;
    }

    // EF CORE
    protected User() { }
}
