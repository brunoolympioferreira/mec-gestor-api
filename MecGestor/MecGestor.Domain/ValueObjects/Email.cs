using System.Text.RegularExpressions;

namespace MecGestor.Domain.ValueObjects;

/// <summary>
/// Value Object Email
/// </summary>
public sealed record Email
{
    private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    private static readonly Regex EmailRegex = new(EmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Address { get; }

    private Email(string address)
    {
        Address = address;
    }

    /// <summary>
    /// Cria uma nova instância de Email após validação.
    /// </summary>
    public static Email Create(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("O email não pode ser vazio.", nameof(address));

        var normalizedEmail = address.Trim().ToLowerInvariant();

        if (!EmailRegex.IsMatch(normalizedEmail))
            throw new ArgumentException("O formato do email é inválido.", nameof(address));

        if (normalizedEmail.Length > 254)
            throw new ArgumentException("O email não pode ter mais de 254 caracteres.", nameof(address));

        return new Email(normalizedEmail);
    }

    /// <summary>
    /// Tenta criar uma instância de Email, retornando false se inválido.
    /// </summary>
    public static bool TryCreate(string address, out Email? email)
    {
        try
        {
            email = Create(address);
            return true;
        }
        catch
        {
            email = null;
            return false;
        }
    }

    /// <summary>
    /// Retorna o domínio do email (parte após o @).
    /// </summary>
    public string GetDomain()
    {
        var atIndex = Address.IndexOf('@');
        return atIndex >= 0 ? Address[(atIndex + 1)..] : string.Empty;
    }

    /// <summary>
    /// Retorna a parte local do email (antes do @).
    /// </summary>
    public string GetLocalPart()
    {
        var atIndex = Address.IndexOf('@');
        return atIndex >= 0 ? Address[..atIndex] : Address;
    }

    public override string ToString() => Address;

    public static implicit operator string(Email email) => email.Address;
}
