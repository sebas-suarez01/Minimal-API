using Minimal_API.Domain.Orders;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Interfaces.Repository;

public interface IOrderRepository : IRepository<OrderModel, Guid>
{
    public Task<Result<IEnumerable<OrderDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<Result<OrderDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}