using Minimal_API.Domain.Users;

namespace Minimal_API.Application.Interfaces;

public interface IJwtProvider
{
    public Task<string> Generate(UserDto user);
}