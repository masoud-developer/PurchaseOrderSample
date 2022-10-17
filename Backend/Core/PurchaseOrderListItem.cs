namespace Core;

public class PurchaseOrderListItem
{
    public PurchaseOrderListItem()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public Guid PurchaseOrderId { get; set; }
    public string Name { get; set; }
    public double Amount { get; set; }
}