namespace Minimal_API.Domain.Common;

public class RegisterModel
{
    public RegisterModel(string username, string name, string lastName, string email, int? phoneNumber, string password, string confirmPassword)
    {
        Username = username;
        Name = name;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
        ConfirmPassword = confirmPassword;
    }
    public string Username { get; private set; }
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public int? PhoneNumber { get; private set; }
    public string Password { get; private set; }
    public string ConfirmPassword { get; private set; }
}