using Minimal_API.Application.Abstractions;

namespace Minimal_API.Application.Auth.Commands.Login;

public record LoginCommand(string Username, string Password):ICommand<string>;