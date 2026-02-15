using FluentValidation;
using MecGestor.Application.Models.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace MecGestor.Api.Middlewares;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public GlobalExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionHandlerMiddleware> logger,
        IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Primeiro checa se é ValidationException (usando a exceção original)
            if (ex is ValidationException validationEx)
            {
                _logger.LogWarning("Erro de validação: {Errors}",
                    string.Join(", ", validationEx.Errors.Select(e => e.ErrorMessage)));

                await HandleExceptionAsync(context, ex);
            }
            else
            {
                // Se não for ValidationException, aí sim desembrulha DbUpdateException
                var exceptionToHandle = ex is DbUpdateException dbEx && dbEx.InnerException != null
                    ? dbEx.InnerException
                    : ex;

                _logger.LogError(ex, "Erro não tratado: {Message}", exceptionToHandle.Message);

                await HandleExceptionAsync(context, exceptionToHandle);
            }
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message, errors) = exception switch
        {
            ValidationException validationEx => HandleValidationException(validationEx),
            UnauthorizedAccessException => (
                HttpStatusCode.Unauthorized,
                "Acesso não autorizado",
                new List<string>()
            ),
            KeyNotFoundException => (
                HttpStatusCode.NotFound,
                "Recurso não encontrado",
                new List<string> { exception.Message }
            ),
            ArgumentException argumentEx => (
                HttpStatusCode.BadRequest,
                "Dados inválidos",
                new List<string> { argumentEx.Message }
            ),
            InvalidOperationException => (
                HttpStatusCode.BadRequest,
                "Operação inválida",
                new List<string> { exception.Message }
            ),
            _ => (
                HttpStatusCode.InternalServerError,
                "Erro interno do servidor",
                _environment.IsDevelopment()
                    ? [exception.Message, exception.StackTrace ?? ""]
                    : new List<string>()
            )
        };

        context.Response.StatusCode = (int)statusCode;

        var response = ApiResponse<object>.ErrorResult(message, errors);

        JsonSerializerOptions options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = _environment.IsDevelopment()
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }

    private (HttpStatusCode, string, List<string>) HandleValidationException(ValidationException validationException)
    {
        var errors = validationException.Errors
            .Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
            .ToList();

        return (HttpStatusCode.BadRequest, "Erro de validação", errors);
    }
}

public static class GlobalExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}