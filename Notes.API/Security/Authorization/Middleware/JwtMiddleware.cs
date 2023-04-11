using Microsoft.Extensions.Options;
using Notes.API.Security.Authorization.Middleware.Handlers.Interfaces;
using Notes.API.Security.Authorization.Settings;
using Notes.API.Security.Domain.Services;

namespace Notes.API.Security.Authorization.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IUserService userService, IJwtHandler handler)
    {
        Console.WriteLine("Goes here.");
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        Console.WriteLine($"Token: {token}");
        var userId = handler.ValidateToken(token!);
        Console.WriteLine(userId);
        if (userId != null)
        {
            // Attach user to context on successful JWT validation
            context.Items["User"] = await userService.GetByIdAsync(userId.Value);
        }
        else
        {
            Console.WriteLine("UserId is null????");
        }

        await _next(context);
    }

}