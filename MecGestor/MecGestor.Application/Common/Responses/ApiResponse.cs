namespace MecGestor.Application.Common.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
    public DateTime Timestamp { get; set; }

    public ApiResponse()
    {
        Timestamp = DateTime.UtcNow;
    }

    /// <summary>
    /// Resposta de sucesso com dados
    /// </summary>
    public static ApiResponse<T> SuccessResult(T data, string? message = null)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Message = message ?? "Operação realizada com sucesso"
        };
    }

    /// <summary>
    /// Resposta de sucesso sem dados
    /// </summary>
    public static ApiResponse<T> SuccessResult(string message = "Operação realizada com sucesso")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message
        };
    }

    /// <summary>
    /// Resposta de erro
    /// </summary>
    public static ApiResponse<T> ErrorResult(string message, List<string>? errors = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors
        };
    }

    /// <summary>
    /// Resposta de erro com uma única mensagem
    /// </summary>
    public static ApiResponse<T> ErrorResult(string message, string error)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = [error]
        };
    }

}
