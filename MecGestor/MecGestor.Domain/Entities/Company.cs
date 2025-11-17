using MecGestor.Domain.Enums;
using MecGestor.Domain.ValueObjects;

namespace MecGestor.Domain.Entities;

public class Company : BaseEntity
{
    public Company(string name, string document, string email, string phone, bool active, PlanEnum plan)
    {
        Name = name;
        Document = Document.Create(document);
        Email = Email.Create(email);
        Phone = phone;
        Active = active;
        Plan = plan;
    }

    /// <summary>
    /// Ef Core constructor
    /// </summary>
    protected Company() { }

    public string Name { get; private set; }
    public Document Document { get; private set; }
    public Email Email { get; private set; }
    public string Phone { get; private set; }
    public bool Active { get; private set; }
    public PlanEnum Plan { get; private set; }
}
