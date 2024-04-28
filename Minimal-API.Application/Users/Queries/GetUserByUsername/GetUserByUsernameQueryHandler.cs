using Minimal_API.Application.Abstractions;
using Minimal_API.Application.Interfaces.Repository;
using Minimal_API.Domain.Shared;
using Minimal_API.Domain.Users;

namespace Minimal_API.Application.Users.Queries.GetUserByUsername;

internal sealed class GetUserByUsernameQueryHandler : IQueryHandler<GetUserByUsernameQuery, UserDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserByUsernameQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDto>> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByUsernameAsync(request.Username, cancellationToken);
    }
}