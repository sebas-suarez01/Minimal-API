using Minimal_API.Application.Abstractions;
using Minimal_API.Application.Interfaces;
using Minimal_API.Domain.Common;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Auth.Queries.Login;

internal sealed class LoginQueryHandler : IQueryHandler<LoginQuery, string>
{
    private readonly IAuthRepository _repository;

    public LoginQueryHandler(IAuthRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        return await _repository.Login(new LoginModel(request.Username, request.Password));
    }
}