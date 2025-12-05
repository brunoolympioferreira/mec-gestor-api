using MecGestor.Domain.Intefaces.Contracts;
using MecGestor.Domain.Intefaces.Repositories;

namespace MecGestor.Infra.Persistence;

public class UnityOfWork : IUnityOfWork
{
    private readonly MecGestorDbContext _context;

    public UnityOfWork(
        MecGestorDbContext context,
        ICompanyRepositroy companyRepositroy,
        IUserRepository userRepository)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        Companies = companyRepositroy;
        Users = userRepository;
    }

    public ICompanyRepositroy Companies { get; }
    public IUserRepository Users { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
