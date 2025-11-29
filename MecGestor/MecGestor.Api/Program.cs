using FluentValidation;
using MecGestor.Api.Filters;
using MecGestor.Api.Middlewares;
using MecGestor.Application;
using MecGestor.Application.Validations.Company;
using MecGestor.Infra;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using Serilog;

try
{
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build())
        .CreateLogger();

    Log.Information("Iniciando MecGestor API");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddValidatorsFromAssemblyContaining<CreateCompanyRequestValidator>();
    builder.Services.AddScoped<ValidationFilter>();

    builder.Services.AddControllers(options =>
    {
        options.Filters.AddService<ValidationFilter>();
    });

    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    builder.Services.AddOpenApi();
    builder.Services.AddInfraModule(builder.Configuration);
    builder.Services.AddApplicationModule();

    var app = builder.Build();

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
}
catch (Exception ex)
{
    Log.Fatal(ex, "Erro fatal ao iniciar a aplicação");
}
finally
{
    Log.CloseAndFlush();
}