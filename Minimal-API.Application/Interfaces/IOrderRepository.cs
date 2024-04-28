using Minimal_API.Domain.Orders;
using Minimal_API.Domain.Shared;
using Minimal_API.Domain.Users;

namespace Minimal_API.Application.Interfaces;

public interface IOrderRepository : IRepository<OrderModel, Guid>
{
    public Task<Result<IEnumerable<OrderDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<Result<OrderDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}