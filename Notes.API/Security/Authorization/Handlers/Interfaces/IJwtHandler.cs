using Notes.API.Security.Models;

namespace Notes.API.Security.Authorization.Middleware.Handlers.Interfaces;

public interface IJwtHandler
{
    string GenerateToken(User user);
    int? ValidateToken(string token);
}