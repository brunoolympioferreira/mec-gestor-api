using MecGestor.Application.Models.Requests;
using MecGestor.Application.Models.Responses;
using MecGestor.Application.Services.Interfaces;

namespace MecGestor.Application.Services.Authentication;

public class LoginService : ILoginService
{
    public Task<ApiResponse<LoginResponse>> Login(LoginRequest model)
    {
        throw new NotImplementedException();
    }
}
