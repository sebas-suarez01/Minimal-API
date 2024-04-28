using Minimal_API.Application.Abstractions;
using Minimal_API.Application.Requests;

namespace Minimal_API.Application.Orders.Commands.AddOrder;

public record AddOrderCommand(Guid UserId, List<ItemRequestService> OrderItems): ICommand<Guid>;