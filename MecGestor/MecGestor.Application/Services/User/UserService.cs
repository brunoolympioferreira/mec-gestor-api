using MecGestor.Application.Models.Requests;
using MecGestor.Application.Services.Interfaces;
using MecGestor.Domain.Intefaces.Contracts;

namespace MecGestor.Application.Services.User;

public class UserService(IUnityOfWork unityOfWork) : IUserService
{
    public async Task<Guid> Create(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = request.ToEntity();

        await CompanyExistsValidation(user);

        await UsernameValidation(user);

        await unityOfWork.Users.AddAsync(user);
        await unityOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    private async Task UsernameValidation(Domain.Entities.User user)
    {
        var userNameExists = await unityOfWork.Users.ExistsByCompanyAsync(user.Username, user.CompanyId);
        if (userNameExists)
        {
            throw new InvalidOperationException($"Username {user.Username} existente para a Company informada");
        }
    }

    private async Task CompanyExistsValidation(Domain.Entities.User user)
    {
        bool companyExists = await unityOfWork.Companies.ExistsAsync(user.CompanyId);
        if (!companyExists)
        {
            throw new KeyNotFoundException("Company informada não existe na base de dados");
        }
    }
}
