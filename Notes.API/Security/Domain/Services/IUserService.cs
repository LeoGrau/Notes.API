using Notes.API.Security.Domain.Services.Communication;
using Notes.API.Security.Domain.Services.Communication.Responses;
using Notes.API.Security.Models;
using Notes.API.Security.Services.Communication;

namespace Notes.API.Security.Domain.Services;

public interface IUserService
{
    Task<IEnumerable<User>> ListAllUsersAsync();
    Task<User?> GetByIdAsync(long userId);
    Task<AuthResponse> AuthenticateAsync(AuthRequest authRequest);
    Task RegisterAsync(RegisterRequest registerRequest);
    Task UpdateAsync(long userId, UpdateRequest updateRequest);
    Task DeleteAsync(long userId);

}