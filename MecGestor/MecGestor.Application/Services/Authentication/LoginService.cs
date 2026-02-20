using MecGestor.Application.Models.Requests;
using MecGestor.Application.Models.Responses;
using MecGestor.Application.Services.Interfaces;
using MecGestor.Domain.Extensions;
using MecGestor.Domain.Intefaces.Contracts;

namespace MecGestor.Application.Services.Authentication;

public class LoginService(IUnityOfWork unityOfWork, IAuthService authService) : ILoginService
{
    public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest model, CancellationToken cancellationToken = default)
    {
        Domain.Entities.User user = await unityOfWork.Users.GetByEmailAndPassword(model.Email);

        if (user is null || !model.Password.VerifyPassword(user.PasswordHash))
            throw new UnauthorizedAccessException("Dados inválidos para login");

        //if (user.RehashPasswordIfNeeded(model.Password)) //TODO
            // Atualizar senha do usuario no banco de dados com um novo hash.

        string token = authService.GenerateJwtToken(user);

        return new ApiResponse<LoginResponse>
        {
            Success = true,
            Data = new LoginResponse(token),
            Message = "Login realizado com sucesso",
            Timestamp = DateTime.UtcNow
        };
    }
}
