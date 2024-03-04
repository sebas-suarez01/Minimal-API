using Minimal_API.Application.Abstractions;

namespace Minimal_API.Application.Users.Commands.ChangeUserRole;

public record ChangeUserRoleCommand(Guid Id, string RoleName) : ICommand;