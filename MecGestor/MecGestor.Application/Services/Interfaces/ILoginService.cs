using MecGestor.Application.Models.Requests;
using MecGestor.Application.Models.Responses;

namespace MecGestor.Application.Services.Interfaces;

public interface ILoginService
{
    Task<ApiResponse<LoginResponse>> Login(LoginRequest model);
}
