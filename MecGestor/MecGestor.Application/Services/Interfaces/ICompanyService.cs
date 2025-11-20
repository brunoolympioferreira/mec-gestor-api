using MecGestor.Application.Common.Requests;

namespace MecGestor.Application.Services.Interfaces;

public interface ICompanyService
{
    Task<Guid> CreateAsync(CompanyRequest request, CancellationToken cancellationToken = default);
}
