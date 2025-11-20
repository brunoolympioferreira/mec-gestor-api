using MecGestor.Domain.Entities;
using MecGestor.Domain.Enums;

namespace MecGestor.Application.Common.Requests;

public record CompanyRequest(
    string Name, 
    string Document, 
    string Email,
    string Phone, 
    bool Active, 
    string Plan)
{
    public Company ToEntity()
    {
        var planEnum = GetPlanEnum();
        return new Company(Name, Document, Email, Phone, Active, planEnum);
    }

    //TODO -> Mover essa validação para FluentValidation
    private PlanEnum GetPlanEnum()
    {
        if (Enum.TryParse<PlanEnum>(Plan, true, out var planEnum) &&
            Enum.IsDefined(planEnum))
        {
            return planEnum;
        }

        throw new ArgumentException(
            $"O plano '{Plan}' não é válido. Valores aceitos: {string.Join(", ", Enum.GetNames<PlanEnum>())}");
    }
}
