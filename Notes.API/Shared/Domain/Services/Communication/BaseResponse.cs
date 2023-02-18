namespace Notes.API.Shared.Domain.Services.Communication;

public class BaseResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Resource { get; set; }

    public BaseResponse(string? message)
    {
        Success = false;
        Message = message;
        Resource = default;
    }

    public BaseResponse(T? resource)
    {
        Success = true;
        Message = string.Empty;
        Resource = resource;
    }
}