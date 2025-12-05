using MecGestor.Application.Models.Requests;
using MecGestor.Application.Models.Responses;
using MecGestor.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MecGestor.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompaniesController(ICompanyService companyService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompanyRequest request, CancellationToken cancellationToken)
    {
        var companyId = await companyService.CreateAsync(request, cancellationToken);

        return CreatedAtAction(nameof(Create), new { id = companyId }, ApiResponse<object>.SuccessResult(companyId, "Empresa criada com sucesso"));
    }
}
