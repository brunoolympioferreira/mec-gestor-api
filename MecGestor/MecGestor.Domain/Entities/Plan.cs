using MecGestor.Domain.Enums;

namespace MecGestor.Domain.Entities;

public class Plan : BaseEntity
{
    public Plan(string name, string description, PlanTypeEnum type, decimal amount)
    {
        Name = name;
        Description = description;
        Type = type;
        Amount = amount;
        Companies = new HashSet<Company>();
    }

    /// <summary>
    /// EF Core Constructor
    /// </summary>
    protected Plan() {}

    public string Name { get; private set; }
    public string Description { get; private set; }
    public PlanTypeEnum Type { get; private set; }
    public decimal Amount { get; private set; }

    public virtual ICollection<Company> Companies { get; set; }
}
