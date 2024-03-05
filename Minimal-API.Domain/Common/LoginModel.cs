namespace Minimal_API.Domain.Common;

public class LoginModel
{
    public LoginModel(string username, string password)
    {
        Username = username;
        Password = password;
    }
    public string Username { get; private set; }
    public string Password { get; private set; }
}