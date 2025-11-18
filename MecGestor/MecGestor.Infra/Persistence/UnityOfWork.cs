using MecGestor.Domain.Intefaces.Contracts;
using MecGestor.Domain.Intefaces.Repositories;

namespace MecGestor.Infra.Persistence;

public class UnityOfWork : IUnityOfWork
{
    private readonly MecGestorDbContext _context;

    private ICompanyRepositroy _companyRepository;

    public UnityOfWork(MecGestorDbContext context, ICompanyRepositroy companyRepositroy)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _companyRepository = companyRepositroy;
    }

    public ICompanyRepositroy Companies => _companyRepository;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
