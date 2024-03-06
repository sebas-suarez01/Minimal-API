namespace Minimal_API.API.Requests;

public record RegisterRequest(string Username,
    string Name,
    string LastName,
    string Email,
    int? PhoneNumber,
    string Password,
    string ConfirmPassword);