using MecGestor.Application.Models.Requests;
using MecGestor.Application.Models.Responses;
using MecGestor.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MecGestor.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var userId = await service.Create(request, cancellationToken);
        return CreatedAtAction(nameof(Create), new { id = userId }, ApiResponse<object>.SuccessResult(userId, "User criado com sucesso"));
    }
}
