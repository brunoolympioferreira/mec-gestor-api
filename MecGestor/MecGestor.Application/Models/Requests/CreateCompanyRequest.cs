using MecGestor.Domain.Entities;
using MecGestor.Domain.Enums;

namespace MecGestor.Application.Models.Requests;

public record CreateCompanyRequest(
    string Name, 
    string Document, 
    string Email,
    string Phone, 
    bool Active, 
    string Plan)
{
    public Company ToEntity()
    {
        var planEnum = Enum.Parse<PlanEnum>(Plan);
        return new Company(Name, Document, Email, Phone, Active, planEnum);
    }
}
