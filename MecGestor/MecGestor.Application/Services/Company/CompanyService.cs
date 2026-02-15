using MecGestor.Application.Models.Requests;
using MecGestor.Application.Services.Interfaces;
using MecGestor.Domain.Events;
using MecGestor.Domain.Intefaces.Contracts;
using MecGestor.Domain.Intefaces.Events;

namespace MecGestor.Application.Services.Company;

public class CompanyService(IUnityOfWork unityOfWork, IEventDispatcher eventDispatcher) : ICompanyService
{
    public async Task<Guid> CreateAsync(CreateCompanyRequest request, CancellationToken cancellationToken = default)
    {
        var company = request.ToEntity();

        var existingCompanyDocument = await unityOfWork.Companies
            .FindAsync(c => c.Document.Value == company.Document.Value);

        if (existingCompanyDocument.Any())
            throw new InvalidOperationException($"Já existe uma empresa cadastrada com este documento: {company.Document}");

        var existingCompanyEmail = await unityOfWork.Companies
            .FindAsync(c => c.Email.Address == company.Email.Address);

        if (existingCompanyDocument.Any())
            throw new InvalidOperationException($"Já existe uma empresa cadastrada com este email: {company.Email}");

        await unityOfWork.Companies.AddAsync(company);

        await unityOfWork.SaveChangesAsync(cancellationToken);

        var companyCreatedEvent = new CompanyCreatedEvent(company.Id, company.Name, company.Email);
        await eventDispatcher.DispatchAsync(companyCreatedEvent, cancellationToken);

        return company.Id;
    }
}
