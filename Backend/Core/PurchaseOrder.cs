using Core.Utils;

namespace Core;

public class PurchaseOrder
{
    public PurchaseOrder()
    {
        Id = Guid.NewGuid();
        CreationTime = DateTime.Now;
        Status = OrderStatus.Draft;
        Name = RandomString.Generate(15);
    }
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreationTime { get; set; }
    public OrderStatus Status { get; set; }
    public string Name { get; set; }
    
    public virtual ICollection<PurchaseOrderListItem> Items { get; set; }
}