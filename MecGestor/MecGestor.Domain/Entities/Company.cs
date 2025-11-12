using MecGestor.Domain.ValueObjects;

namespace MecGestor.Domain.Entities;

public class Company : BaseEntity
{
    public Company(string name, string document, string email, string phone, bool active, Guid planId)
    {
        Name = name;
        Document = Document.Create(document);
        Email = Email.Create(email);
        Phone = phone;
        Active = active;
        PlanId = planId;
    }

    public string Name { get; private set; }
    public Document Document { get; private set; }
    public Email Email { get; private set; }
    public string Phone { get; private set; }
    public bool Active { get; private set; }
    public Guid PlanId { get; private set; }

    /// <summary>
    /// Relacionamento 1:N com o plano da empresa
    /// </summary>
    public required virtual Plan Plan { get; set; }
}
