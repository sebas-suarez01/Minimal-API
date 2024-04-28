using Minimal_API.Application.Abstractions;
using Minimal_API.Application.Interfaces.Repository;
using Minimal_API.Domain.Shared;
using Minimal_API.Domain.Users;

namespace Minimal_API.Application.Users.Queries.GetAllUsers;

internal sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _repository;

    public GetAllUsersQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public Task<Result<IEnumerable<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken = default)
    {
        return _repository.GetAllAsync(cancellationToken);
    }
}