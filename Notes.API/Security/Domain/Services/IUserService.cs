using Notes.API.Security.Models;
using Notes.API.Security.Services.Communication;

namespace Notes.API.Security.Domain.Services;

public interface IUserService
{
    Task<IEnumerable<User?>> ListAllUsersAsync();
    Task<UserResponse> FindUserAsync(long userId);
    Task<UserResponse> AddAsync(User newUser);
    Task<UserResponse> Update(long userId, User updatedUser);
    Task<UserResponse> Remove(long userId);
}