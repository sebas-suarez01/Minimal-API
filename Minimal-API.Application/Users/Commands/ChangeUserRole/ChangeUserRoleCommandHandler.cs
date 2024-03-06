using Minimal_API.Application.Abstractions;
using Minimal_API.Application.Interfaces;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Users.Commands.ChangeUserRole;

internal sealed class ChangeUserRoleCommandHandler : ICommandHandler<ChangeUserRoleCommand>
{
    private readonly IUserRepository _repository;

    public ChangeUserRoleCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
    {
        await _repository.ChangeRoleAsync(request.Id, request.RoleName);
        
        return Result.Success();
    }
}