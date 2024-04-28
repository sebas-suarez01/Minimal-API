using Minimal_API.Application.Abstractions;
using Minimal_API.Application.Interfaces.Repository;
using Minimal_API.Domain.Shared;
using Minimal_API.Domain.Users;

namespace Minimal_API.Application.Users.Queries.GetUserByEmail;

internal sealed class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, UserDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserByEmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDto>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
    }
}