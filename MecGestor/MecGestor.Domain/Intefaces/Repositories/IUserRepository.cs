using MecGestor.Domain.Entities;

namespace MecGestor.Domain.Intefaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> ExistsByCompanyAsync(string username, Guid companyId);
    Task<User> GetByEmailAndPassword(string email);
}
