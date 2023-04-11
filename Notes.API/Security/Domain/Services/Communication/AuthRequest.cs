using System.ComponentModel.DataAnnotations;

namespace Notes.API.Security.Domain.Services.Communication;

public class AuthRequest
{
    [Required]
    public String? Email { get; set; }
    [Required]
    public String? Password { get; set; }
}