using Minimal_API.Domain.Common;
using Minimal_API.Domain.Roles.Shared;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Interfaces;

public interface IAuthRepository
{
    public Task<Result<Guid>> Register(RegisterModel registerModel, RoleEnum role = RoleEnum.User);
    public Task Login(LoginModel loginModel);
}