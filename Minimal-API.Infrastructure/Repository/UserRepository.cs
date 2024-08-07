﻿using Microsoft.EntityFrameworkCore;
using Minimal_API.Application.Interfaces;
using Minimal_API.Application.Interfaces.Repository;
using Minimal_API.Domain.Errors;
using Minimal_API.Domain.Permission;
using Minimal_API.Domain.Primitives;
using Minimal_API.Domain.Roles;
using Minimal_API.Domain.Shared;
using Minimal_API.Domain.Users;
using Minimal_API.Persistance;

namespace Minimal_API.Infrastructure.Repository;

public class UserRepository : BaseRepository<UserModel, Guid>, IUserRepository
{
    public UserRepository(AgencyDbContext context) : base(context)
    {
    }

    public async Task<Result<IEnumerable<UserDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<UserModel>()
            .Select(u => new UserDto(
                u.Id,
                u.Username,
                u.Name,
                u.LastName,
                u.Email,
                u.PasswordHash,
                u.PhoneNumber,
                new RoleDto(u.Role.Name)))
            .ToListAsync(cancellationToken);
    }

    public async Task<Result<UserDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _context.Set<UserModel>()
            .Include(u => u.Role)
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

        return user is not null
            ? new UserDto(
                user.Id,
                user.Username,
                user.Name,
                user.LastName,
                user.Email,
                user.PasswordHash,
                user.PhoneNumber,
                new RoleDto(user.Role.Name))
            : Result.Failure<UserDto>(ErrorTypes.Models.IdNotFound(id));
    }

    public async Task<Result<UserDto>> GetByUsernameAsync(string username,
        CancellationToken cancellationToken = default)
    {
        var user = await _context.Set<UserModel>()
            .AsNoTracking()
            .Include(u => u.Role)
            .SingleOrDefaultAsync(u => u.Username == username, cancellationToken);

        return user is not null
            ? new UserDto(
                user.Id,
                user.Username,
                user.Name,
                user.LastName,
                user.Email,
                user.PasswordHash,
                user.PhoneNumber,
                new RoleDto(user.Role.Name))
            : Result.Failure<UserDto>(ErrorTypes.Models.UserNotFound(username));
    }

    public async Task<Result<UserDto>> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _context.Set<UserModel>()
            .AsNoTracking()
            .Include(u => u.Role)
            .SingleOrDefaultAsync(u => u.Email == email, cancellationToken);

        return user is not null
            ? new UserDto(
                user.Id,
                user.Username,
                user.Name,
                user.LastName,
                user.Email,
                user.PasswordHash,
                user.PhoneNumber,
                new RoleDto(user.Role.Name))
            : Result.Failure<UserDto>(ErrorTypes.Models.UserNotFound(email));
    }

    public async Task<Result> ChangeRoleAsync(Guid id, string roleName, CancellationToken cancellationToken = default)
    {
        var user = await _context.Set<UserModel>()
            .Include(u => u.Role)
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (user is null)
        {
            return Result.Failure(ErrorTypes.Models.IdNotFound(id));
        }

        var role = await _context.Set<RoleModel>()
            .SingleOrDefaultAsync(u => u.Name.ToLower() == roleName.ToLower(), cancellationToken);

        if (role is null)
        {
            return Result.Failure(ErrorTypes.Models.RoleNotFound(roleName));
        }

        user.Role = role;

        return Result.Success();
    }

    public async Task<Result<List<string>>> GetPermissionsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var permissions = await _context.Set<UserPermission>()
            .Include(up => up.Permission)
            .Where(up => up.UserId == id)
            .Select(up => up.Permission.Name)
            .ToListAsync(cancellationToken);

        return permissions;
    }
}