using Minimal_API.Application.Abstractions;
using Minimal_API.Application.Interfaces;
using Minimal_API.Domain.LineItems;
using Minimal_API.Domain.Orders;
using Minimal_API.Domain.Shared;

namespace Minimal_API.Application.Orders.Commands.AddOrder;

public class AddOrderCommandHandler : ICommandHandler<AddOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IItemRepository _itemRepository;

    public AddOrderCommandHandler(IItemRepository itemRepository, IOrderRepository orderRepository)
    {
        _itemRepository = itemRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Result<Guid>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        var lineItems = new List<LineItemModel>();
        var orderId = new Guid();
        decimal totalPrice = 0;
        
        foreach (var itemRequest in request.OrderItems)
        {
            var itemResult = await _itemRepository.GetByNameAsync(itemRequest.Name, cancellationToken);
            
            if (itemResult.IsFailure)
                return Result.Failure<Guid>(itemResult.Errors);

            var item = itemResult.Value;
            
            lineItems.Add(LineItemModel.Create(item.Id, orderId, itemRequest.Amount));

            totalPrice += item.Price * itemRequest.Amount;
        }

        var order = OrderModel.Create(orderId, totalPrice, request.UserId, lineItems);
        
        await _orderRepository.CreateAsync(order, cancellationToken);

        return order.Id;
    }
}