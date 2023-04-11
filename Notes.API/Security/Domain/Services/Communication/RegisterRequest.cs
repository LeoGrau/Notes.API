using System.ComponentModel.DataAnnotations;

namespace Notes.API.Security.Domain.Services.Communication;

public class RegisterRequest
{
    [Required] public String? Firstname { get; set; }
    [Required] public String? Lastname { get; set; }
    [Required] public String? Email { get; set; }
    [Required] public String? Password { get; set; }
}