using FluentValidation;
using MecGestor.Api.Filters;
using MecGestor.Application;
using MecGestor.Application.Common.Requests;
using MecGestor.Application.Validations.Company;
using MecGestor.Infra;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<CreateCompanyRequestValidator>();
builder.Services.AddScoped<ValidationFilter>();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.AddService<ValidationFilter>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
