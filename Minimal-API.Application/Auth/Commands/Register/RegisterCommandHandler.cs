using Minimal_API.Application.Abstractions;
using Minimal_API.Application.Interfaces.Repository;
using Minimal_API.Domain.Common;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Auth.Commands.Register;

internal sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, Guid>
{
    private readonly IAuthRepository _repository;

    public RegisterCommandHandler(IAuthRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _repository.Register(new RegisterModel(request.Username, request.Name, request.LastName, request.Email,
            request.PhoneNumber, request.Password, request.ConfirmPassword));
    }
}