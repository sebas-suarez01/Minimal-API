﻿using Minimal_API.Application.Abstractions;
using Minimal_API.Domain.Users;

namespace Minimal_API.Application.Users.Queries.GetUserByEmail;

public record GetUserByEmailQuery(string Email) : IQuery<UserDto>;