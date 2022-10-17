using Core;

namespace Business.Interfaces;

public interface IPurchaseOrderBusiness
{
    Task<Result<PurchaseOrder>> CreateOrUpdate(PurchaseOrder order);
    Task<Result<PurchaseOrder>> Submit(Guid orderId);
    Task<Result<List<PurchaseOrder>>> List();
    Task<Result<PurchaseOrder>> Get(Guid orderId);
}