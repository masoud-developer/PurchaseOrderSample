using Business.DataAccess;
using Business.Interfaces;
using Core;
using Microsoft.EntityFrameworkCore;

namespace Business.Classes;

public class PurchaseOrderBusiness : IPurchaseOrderBusiness
{
    private readonly AppDbContext _dbContext;
    private Guid _currentUserId;
    public PurchaseOrderBusiness(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<PurchaseOrder>> Create(PurchaseOrder order)
    {
        if(order.Status == OrderStatus.Submitted)
            return Result<PurchaseOrder>.Failed("Order and list items cannot be changed because order submitted.");
        
        if (order.Items.Count < 1)
            return Result<PurchaseOrder>.Failed("Order must have at least 1 item.");
        
        if(order.Items.Count > 10)
            return Result<PurchaseOrder>.Failed("Order must have maximum 10 item.");
        
        if(order.Items.Sum(item => item.Amount) > 10000)
            return Result<PurchaseOrder>.Failed("sum of order items amount cannot exceed 10000.");
        
        //check user orders
        
        //save in db
        await _dbContext.PurchaseOrders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return Result<PurchaseOrder>.Success(order, "Order created successfully.");
    }
    
    public async Task<Result<PurchaseOrder>> Submit(Guid orderId)
    {
        var order = await _dbContext.PurchaseOrders.FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == _currentUserId);
        if(order == null)
            return Result<PurchaseOrder>.Failed("Order not found.");
        
        order.Status = OrderStatus.Submitted;
        
        //save db
        await _dbContext.SaveChangesAsync();

        return Result<PurchaseOrder>.Success(order, "Order submitted successfully.");
    }
}