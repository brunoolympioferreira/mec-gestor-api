using MecGestor.Application.Models.Requests;
using MecGestor.Application.Models.Responses;
using MecGestor.Application.Services.Interfaces;
using MecGestor.Domain.Intefaces.Contracts;

namespace MecGestor.Application.Services.Authentication;

public class LoginService(IUnityOfWork unityOfWork, IAuthService authService) : ILoginService
{
    public async Task<ApiResponse<LoginResponse>> Login(LoginRequest model)
    {
        Domain.Entities.User user = await unityOfWork.Users.GetByEmailAndPassword(model.Email, model.Password, model.CompanyId)
            ?? throw new UnauthorizedAccessException("Dados inválidos para login");

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
