using Minimal_API.Application.Abstractions;

namespace Minimal_API.Application.Auth.Commands.Register;

public record RegisterCommand(string Username,
    string Name,
    string LastName,
    string Email,
    int? PhoneNumber,
    string Password,
    string ConfirmPassword):ICommand<Guid>;