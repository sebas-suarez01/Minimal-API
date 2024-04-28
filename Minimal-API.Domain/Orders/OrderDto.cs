using Minimal_API.Domain.LineItems;
using Minimal_API.Domain.Users;

namespace Minimal_API.Domain.Orders;

public record OrderDto(Guid Id, decimal TotalPrice, UserDto User, IEnumerable<LineItemDto> OrderItems);