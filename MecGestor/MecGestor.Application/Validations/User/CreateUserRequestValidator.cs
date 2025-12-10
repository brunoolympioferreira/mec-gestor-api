using FluentValidation;
using MecGestor.Application.Models.Requests;
using MecGestor.Domain.Enums;

namespace MecGestor.Application.Validations.User;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty().WithMessage("Username é obrigatório.")
            .MaximumLength(100).WithMessage("User deve conter no máximo 100 caracteres");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email é obrigatório.")
            .EmailAddress().WithMessage("Email inválido.");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Password é obrigatório");

        RuleFor(u => u.Role)
            .NotEmpty().WithMessage("Role é obrigatório.")
            .Must(BeValidRole).WithMessage(r =>
                $"O plano '{r.Role}' não é válido. Valores aceitos: {string.Join(", ", Enum.GetNames<RoleEnum>())}");
    }

    private bool BeValidRole(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            return false;

        return Enum.TryParse<RoleEnum>(role, true, out var roleEnum) &&
               Enum.IsDefined(roleEnum);
    }
}
