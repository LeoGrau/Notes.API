namespace Notes.API.Security.Models;

public class User
{
    public long UserId { get; set; }
    public String? Firstname { get; set; }
    public String? Lastname { get; set; }
    public String? Email { get; set; }
    public String? Password { get; set; }

    public void SetUser(User user)
    {
        Firstname = user.Firstname;
        Lastname = user.Lastname;
        Email = user.Email;
        Password = user.Password;
    }
}