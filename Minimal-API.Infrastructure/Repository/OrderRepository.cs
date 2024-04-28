using Microsoft.EntityFrameworkCore;
using Minimal_API.Application.Interfaces;
using Minimal_API.Domain.Errors;
using Minimal_API.Domain.Items;
using Minimal_API.Domain.LineItems;
using Minimal_API.Domain.Orders;
using Minimal_API.Domain.Roles;
using Minimal_API.Domain.Shared;
using Minimal_API.Domain.Users;
using Minimal_API.Persistance;

namespace Minimal_API.Infrastructure.Repository;

public class OrderRepository : BaseRepository<OrderModel, Guid>, IOrderRepository
{
    public OrderRepository(AgencyDbContext context) : base(context)
    {
    }

    public async Task<Result<IEnumerable<OrderDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var orders = await _context.Set<OrderModel>()
            .Select(o => 
                new OrderDto(
                    o.Id, 
                    o.TotalPrice,
                    new UserDto(
                        o.UserId, 
                        o.User.Username, 
                        o.User.Name, 
                        o.User.LastName, 
                        o.User.Email, 
                        o.User.PasswordHash,
                        o.User.PhoneNumber,
                        new RoleDto(o.User.Role.Name)),
                    o.OrderItems.Select(oi =>
                        new LineItemDto(
                            oi.Id,
                            new ItemDto(
                                oi.ItemId,
                                oi.Item.Name,
                                oi.Item.Price),
                            oi.Amount))))
            .ToListAsync(cancellationToken);

        return orders;
    }

    public async Task<Result<OrderDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await _context.Set<OrderModel>()
            .Where(o=> o.Id.Equals(id))
            .Select(o => 
                new OrderDto(
                    o.Id, 
                    o.TotalPrice,
                    new UserDto(
                        o.UserId, 
                        o.User.Username, 
                        o.User.Name, 
                        o.User.LastName, 
                        o.User.Email, 
                        o.User.PasswordHash,
                        o.User.PhoneNumber,
                        new RoleDto(o.User.Role.Name)),
                    o.OrderItems.Select(oi =>
                            new LineItemDto(
                                oi.Id,
                                new ItemDto(oi.ItemId, oi.Item.Name, oi.Item.Price),
                                oi.Amount))))
            .FirstOrDefaultAsync(cancellationToken);

        return order ?? Result.Failure<OrderDto>(ErrorTypes.Models.IdNotFound(id));
    }
}