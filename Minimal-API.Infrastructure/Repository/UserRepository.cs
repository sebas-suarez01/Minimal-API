using Microsoft.EntityFrameworkCore;
using Minimal_API.Application.Interfaces;
using Minimal_API.Domain.Errors;
using Minimal_API.Domain.Primitives;
using Minimal_API.Domain.Roles;
using Minimal_API.Domain.Shared;
using Minimal_API.Domain.Users;
using Minimal_API.Persistance;

namespace Minimal_API.Infrastructure.Repository;

public class UserRepository : BaseRepository<UserModel, UserId>, IUserRepository
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
            .SingleOrDefaultAsync(u => u.Id == ValueObjectId.Create<UserId>(id), cancellationToken);

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
}