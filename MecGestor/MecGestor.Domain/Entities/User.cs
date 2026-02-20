using MecGestor.Domain.Extensions;
using MecGestor.Domain.ValueObjects;
using System.Net.NetworkInformation;

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

    public User(string email, string password)
    {
        Email = Email.Create(email);
        PasswordHash= password.HashPassword();
    }

    // EF CORE
    protected User() { }

    /// <summary>
    /// Atualiza o hash da senha se o workFactor mudou
    /// </summary>
    /// <param name="plainPassword">Senha em texto puro para gerar novo hash</param>
    /// <param name="workFactor">WorkFactor atual esperado</param>
    /// <returns>True se o hash foi atualizado</returns>
    public bool RehashPasswordIfNeeded(string plainPassword, int workFactor = 12)
    {
        if (!PasswordHash.NeedsRehash(workFactor))
            return false;

        PasswordHash = plainPassword.HashPassword();
        return true;
    }
}
