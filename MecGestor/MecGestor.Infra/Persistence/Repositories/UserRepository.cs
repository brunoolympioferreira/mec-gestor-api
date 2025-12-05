using MecGestor.Domain.Entities;
using MecGestor.Domain.Intefaces.Repositories;

namespace MecGestor.Infra.Persistence.Repositories;

public class UserRepository(MecGestorDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
}
