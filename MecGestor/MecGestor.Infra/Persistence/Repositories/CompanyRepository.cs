using MecGestor.Domain.Entities;
using MecGestor.Domain.Intefaces.Repositories;

namespace MecGestor.Infra.Persistence.Repositories;

public class CompanyRepository(MecGestorDbContext dbContext) : Repository<Company>(dbContext), ICompanyRepositroy
{
}
