using FluentValidation;
using MecGestor.Application.Models.Requests;
using MecGestor.Domain.Enums;

namespace MecGestor.Application.Validations.Company;

public class CreateCompanyRequestValidator : AbstractValidator<CreateCompanyRequest>
{
    public CreateCompanyRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome da empresa é obrigatório.")
            .MaximumLength(200).WithMessage("O nome da empresa deve ter no máximo 200 caracteres.");

        RuleFor(x => x.Document)
            .NotEmpty().WithMessage("O documento é obrigatório.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail informado é inválido.")
            .MaximumLength(254).WithMessage("O e-mail deve ter no máximo 254 caracteres.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("O telefone é obrigatório.")
            .MaximumLength(20).WithMessage("O telefone deve ter no máximo 20 caracteres");

        RuleFor(x => x.Plan)
            .NotEmpty().WithMessage("O plano é obrigatório.")
            .Must(BeValidPlan).WithMessage(r =>
                $"O plano '{r.Plan}' não é válido. Valores aceitos: {string.Join(", ", Enum.GetNames<PlanEnum>())}");
    }
    private bool BeValidPlan(string plan)
    {
        if (string.IsNullOrWhiteSpace(plan))
            return false;

        return Enum.TryParse<PlanEnum>(plan, true, out var planEnum) &&
               Enum.IsDefined(planEnum);
    }
}
