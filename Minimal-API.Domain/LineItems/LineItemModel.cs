using Minimal_API.Domain.Items;
using Minimal_API.Domain.Orders;
using Minimal_API.Domain.Primitives;

namespace Minimal_API.Domain.LineItems;

public class LineItemModel : Entity<Guid>
{
    private LineItemModel()
    {
    }
    
    private LineItemModel(Guid id, ItemModel item, OrderModel order, int amount)
    {
        Item = item;
        Order = order;
        Amount = amount;
        Id = id;
    }
    public Guid ItemId { get; set; }
    public ItemModel Item { get; set; }
    public Guid OrderId { get; set; }
    public OrderModel Order { get; set; }
    public int Amount { get; set; }

    public static LineItemModel Create(Guid itemId, Guid orderId, int amount)
    {
        return new LineItemModel()
        {
            Id = new Guid(),
            ItemId = itemId,
            OrderId = orderId,
            Amount = amount
        };
    }
}