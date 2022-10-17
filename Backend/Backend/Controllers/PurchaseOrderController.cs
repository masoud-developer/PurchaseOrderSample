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

    [HttpPost("create-or-update")]
    public async Task<Result<PurchaseOrder>> Create(PurchaseOrder order)
    {
        return await _purchaseOrderBusiness.CreateOrUpdate(order);
    }
    
    [HttpGet("get/{orderId:guid}")]
    public async Task<Result<PurchaseOrder>> Get(Guid orderId)
    {
        return await _purchaseOrderBusiness.Get(orderId);
    }
    
    [HttpGet("list")]
    public async Task<Result<List<PurchaseOrder>>> List()
    {
        return await _purchaseOrderBusiness.List();
    }
    
    [HttpPut("submit/{orderId:guid}")]
    public async Task<Result<PurchaseOrder>> Submit(Guid orderId)
    {
        return await _purchaseOrderBusiness.Submit(orderId);
    }
}