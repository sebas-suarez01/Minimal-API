using Minimal_API.Application.Abstractions;
using Minimal_API.Application.Interfaces.Repository;
using Minimal_API.Domain.Common;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Auth.Commands.Login;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
{
    private readonly IAuthRepository _repository;

    public LoginCommandHandler(IAuthRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _repository.Login(new LoginModel(request.Username, request.Password));
    }
}