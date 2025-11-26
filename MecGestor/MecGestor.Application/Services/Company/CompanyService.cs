using MecGestor.Application.Common.Requests;
using MecGestor.Application.Services.Interfaces;
using MecGestor.Domain.Intefaces.Contracts;

namespace MecGestor.Application.Services.Company;

public class CompanyService(IUnityOfWork unityOfWork) : ICompanyService
{
    public async Task<Guid> CreateAsync(CreateCompanyRequest request, CancellationToken cancellationToken = default)
    {
        var company = request.ToEntity();

        await unityOfWork.Companies.AddAsync(company);

        await unityOfWork.SaveChangesAsync(cancellationToken);

        return company.Id;
    }
}
