using Minimal_API.Domain.Users;

namespace Minimal_API.Application.Interfaces;

public interface IJwtProvider
{
    public string Generate(UserDto user);
}