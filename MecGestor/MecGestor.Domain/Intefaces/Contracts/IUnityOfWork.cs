using MecGestor.Domain.Intefaces.Repositories;

namespace MecGestor.Domain.Intefaces.Contracts;

public interface IUnityOfWork
{
    ICompanyRepositroy Companies { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
