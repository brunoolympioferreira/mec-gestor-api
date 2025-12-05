using MecGestor.Domain.Intefaces.Repositories;

namespace MecGestor.Domain.Intefaces.Contracts;

public interface IUnityOfWork
{
    ICompanyRepositroy Companies { get; }
    IUserRepository Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
