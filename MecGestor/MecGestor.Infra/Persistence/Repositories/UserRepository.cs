using MecGestor.Domain.Entities;
using MecGestor.Domain.Intefaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MecGestor.Infra.Persistence.Repositories;

public class UserRepository(MecGestorDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    public async Task<bool> ExistsByCompanyAsync(string username, Guid companyId)
    {
        return await _dbSet.AnyAsync(e => e.Username.Trim() == username.Trim() && e.CompanyId == companyId);
    }
}
