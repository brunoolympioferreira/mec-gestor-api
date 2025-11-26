using MecGestor.Application.Common.Requests;

namespace MecGestor.Application.Services.Interfaces;

public interface ICompanyService
{
    Task<Guid> CreateAsync(CreateCompanyRequest request, CancellationToken cancellationToken = default);
}
