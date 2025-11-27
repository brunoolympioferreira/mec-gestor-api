using FluentValidation;
using MecGestor.Api.Filters;
using MecGestor.Api.Middlewares;
using MecGestor.Application;
using MecGestor.Application.Validations.Company;
using MecGestor.Infra;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<CreateCompanyRequestValidator>();

builder.Services.AddScoped<ValidationFilter>();

builder.Services.AddControllers(options =>
{
    options.Filters.AddService<ValidationFilter>();
});

// Customiza o comportamento de Model State para não retornar automaticamente BadRequest
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    // Desabilita a resposta automática de validação do ASP.NET Core
    // para que o middleware possa capturar ValidationException
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddOpenApi();

builder.Services.AddInfraModule(builder.Configuration);
builder.Services.AddApplicationModule();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.Title = "MecGestor API";
        options.DarkMode = true;
    });
}
app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
