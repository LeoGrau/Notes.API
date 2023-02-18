using Notes.API.Security.Models;
using Notes.API.Shared.Domain.Services.Communication;

namespace Notes.API.Security.Services.Communication;

public class UserResponse : BaseResponse<User>
{
    public UserResponse(string? message) : base(message)
    {
    }

    public UserResponse(User? resource) : base(resource)
    {
    }
}