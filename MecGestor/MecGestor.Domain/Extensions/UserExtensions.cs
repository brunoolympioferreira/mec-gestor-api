namespace MecGestor.Domain.Extensions;

public static class UserExtensions
{
    /// <summary>
    /// Faz o hash da senha utilizando BCrypt
    /// </summary>
    /// <param name="password"></param>
    public static string HashPassword(this string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
    }

    /// <summary>
    /// Verifica se a senha corresponde ao hash armazenado
    /// </summary>
    /// <param name="password"></param>
    /// <param name="passwordHash"></param>
    /// <returns>True or False</returns>
    public static bool VerifyPassword(this string password, string passwordHash)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Método para verificar se um hash precisa ser refeito (se o work factor mudou)
    /// </summary>
    /// <param name="passwordHash"></param>
    /// <param name="workFactor"></param>
    /// <returns>True or False (se houver erro, é melhor refazer o hash</returns>
    public static bool NeedsRehash(this string passwordHash, int workFactor = 12)
    {
        try
        {
            return BCrypt.Net.BCrypt.PasswordNeedsRehash(passwordHash, workFactor);
        }
        catch
        {
            return true;
        }
    }
}
