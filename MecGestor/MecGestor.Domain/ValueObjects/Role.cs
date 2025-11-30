using MecGestor.Domain.Enums;

namespace MecGestor.Domain.ValueObjects;

public class Role : IEquatable<Role>
{
    public RoleEnum Value { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    private static readonly Dictionary<RoleEnum, string> RoleDescriptions = new()
    {
        { RoleEnum.Administrator, "Acesso total ao sistema" },
        { RoleEnum.Mechanic, "Acesso para mecânicos" },
        { RoleEnum.Employee, "Acesso para funcionários" }
    };

    private Role(RoleEnum role)
    {
        Value = role;
        Name = role.ToString();
        Description = RoleDescriptions[role];
    }

    public static Role Create(RoleEnum role)
    {
        if (!Enum.IsDefined(typeof(RoleEnum), role))
            throw new ArgumentException("Perfil de acesso inválido", nameof(role));

        return new Role(role);
    }

    public static Role Create(int roleId)
    {
        if (!Enum.IsDefined(typeof(RoleEnum), roleId))
            throw new ArgumentException("Perfil de acesso inválido", nameof(roleId));

        return new Role((RoleEnum)roleId);
    }

    public static Role Create(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            throw new ArgumentException("Nome do perfil não pode ser vazio", nameof(roleName));

        if (!Enum.TryParse<RoleEnum>(roleName, true, out var role))
            throw new ArgumentException("Perfil de acesso inválido", nameof(roleName));

        return new Role(role);
    }

    public bool IsAdministrator() => Value == RoleEnum.Administrator;
    public bool IsMechanic() => Value == RoleEnum.Mechanic;
    public bool IsEmployee() => Value == RoleEnum.Employee;

    public bool HasPermission(RoleEnum requiredRole)
    {
        if (Value == RoleEnum.Administrator)
            return true;

        return Value <= requiredRole;
    }

    public bool Equals(Role? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Role);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(Role? left, Role? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(Role? left, Role? right)
    {
        return !(left == right);
    }

    public override string ToString() => Name;

    public static Role Administrator => Create(RoleEnum.Administrator);
    public static Role Mechanic => Create(RoleEnum.Mechanic);
    public static Role Employee => Create(RoleEnum.Employee);
}