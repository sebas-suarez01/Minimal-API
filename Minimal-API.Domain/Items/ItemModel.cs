using Minimal_API.Domain.LineItems;
using Minimal_API.Domain.Primitives;

namespace Minimal_API.Domain.Items;

public class ItemModel : Entity<Guid>
{
    private ItemModel()
    {
    }
    
    private ItemModel(Guid id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
        ItemOrders = new List<LineItemModel>();
    }
    
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<LineItemModel> ItemOrders { get; set; }

    public static ItemModel Create(string name, decimal price)
    {
        return new ItemModel(new Guid(), name, price);
    }
}