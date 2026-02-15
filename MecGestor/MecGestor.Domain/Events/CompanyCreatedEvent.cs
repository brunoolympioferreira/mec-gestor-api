namespace MecGestor.Domain.Events;

/// <summary>
/// Evento disparado quando uma nova Company é criada
/// </summary>
public sealed class CompanyCreatedEvent
{
    public Guid CompanyId { get; }
    public string CompanyName { get; }
    public string CompanyEmail { get; set; }
    public DateTime CreatedAt { get; }

    public CompanyCreatedEvent(Guid companyId, string companyName, string companyEmail)
    {
        CompanyId = companyId;
        CompanyName = companyName;
        CompanyEmail = companyEmail;
        CreatedAt = DateTime.UtcNow;
    }
}
