using Minimal_API.Domain.LineItems;
using Minimal_API.Domain.Primitives;
using Minimal_API.Domain.Users;

namespace Minimal_API.Domain.Orders;

public class OrderModel : Entity<Guid>
{
    private OrderModel()
    {
    }
    
    private OrderModel(Guid id, decimal totalPrice, UserModel user, List<LineItemModel> orderItems)
    {
        Id = id;
        TotalPrice = totalPrice;
        User = user;
        OrderItems = orderItems;
    }
    public decimal TotalPrice { get; set; }
    public Guid UserId { get; set; }
    public UserModel User { get; set; }
    public List<LineItemModel> OrderItems { get; set; }

    public static OrderModel Create(decimal totalPrice, Guid userId, List<LineItemModel> orderItems)
    {
        return new OrderModel()
        {
            Id = new Guid(),
            TotalPrice = totalPrice,
            UserId = userId,
            OrderItems = orderItems
        };
    }
    public static OrderModel Create(Guid id, decimal totalPrice, Guid userId, List<LineItemModel> orderItems)
    {
        
        
        return new OrderModel()
        {
            Id = id,
            TotalPrice = totalPrice,
            UserId = userId,
            OrderItems = orderItems
        };
    }
}