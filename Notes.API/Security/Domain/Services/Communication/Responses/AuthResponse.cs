namespace Notes.API.Security.Domain.Services.Communication.Responses;

public class AuthResponse
{
    public int UserId { get; set; }
    public String? FirstName { get; set; }
    public String? LastName { get; set; }
    public String? Email { get; set; }
    public String? Token { get; set; }
}