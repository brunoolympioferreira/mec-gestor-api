using MecGestor.Application.Models.Requests;
using MecGestor.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MecGestor.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController(ILoginService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest model, CancellationToken cancellationToken)
    {
        var response = await service.LoginAsync(model, cancellationToken);
        return Ok(response);
    }
}
