using Business.Interfaces;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PurchaseOrderController : ControllerBase
{
    private readonly IPurchaseOrderBusiness _purchaseOrderBusiness;

    public PurchaseOrderController(IPurchaseOrderBusiness purchaseOrderBusiness)
    {
        _purchaseOrderBusiness = purchaseOrderBusiness;
    }

    [HttpPost("create")]
    public async Task<Result<PurchaseOrder>> Create(PurchaseOrder order)
    {
        return await _purchaseOrderBusiness.Create(order);
    }
    
    [HttpPut("submit/{orderId:guid}")]
    public async Task<Result<PurchaseOrder>> Submit(Guid orderId)
    {
        return await _purchaseOrderBusiness.Submit(orderId);
    }
}