using Minimal_API.Application.Abstractions;

namespace Minimal_API.Application.Auth.Queries.Login;

public record LoginQuery(string Username, string Password):IQuery<string>;