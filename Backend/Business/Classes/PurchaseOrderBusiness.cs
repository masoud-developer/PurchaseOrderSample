using Business.DataAccess;
using Business.Interfaces;
using Core;
using Microsoft.EntityFrameworkCore;

namespace Business.Classes;

public class PurchaseOrderBusiness : IPurchaseOrderBusiness
{
    private readonly AppDbContext _dbContext;
    
    //default userId
    private Guid _currentUserId = Guid.Empty;

    public PurchaseOrderBusiness(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PurchaseOrder>> CreateOrUpdate(PurchaseOrder order)
    {
        PurchaseOrder dbOrder = null;
        if (order.Id != Guid.Empty)
        {
            dbOrder = await _dbContext.PurchaseOrders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == order.Id);
            if (dbOrder != null && dbOrder.Status == OrderStatus.Submitted)
                return Result<PurchaseOrder>.Failed("Order and list items cannot be changed because order submitted.");
        }

        if (order.Items.Count < 1)
            return Result<PurchaseOrder>.Failed("Order must have at least 1 item.");

        if (order.Items.Count > 10)
            return Result<PurchaseOrder>.Failed("Order must have maximum 10 item.");

        if (order.Items.Sum(item => item.Amount) > 10000)
            return Result<PurchaseOrder>.Failed("sum of order items amount cannot exceed 10000.");

        order.UserId = _currentUserId;

        //save in db
        if (dbOrder == null)
        {
            order.Id = Guid.NewGuid();
            await _dbContext.PurchaseOrders.AddAsync(order);
        }
        else
        {
            dbOrder.Items.Clear();
            foreach (var item in order.Items)
            {
                dbOrder.Items.Add(item);
            }
        }

        await _dbContext.SaveChangesAsync();

        return Result<PurchaseOrder>.Success(order, "Order created successfully.");
    }

    public async Task<Result<List<PurchaseOrder>>> List()
    {
        var orders = await _dbContext.PurchaseOrders.ToListAsync();
        return Result<List<PurchaseOrder>>.Success(orders, "List successfully fetched.");
    }

    public async Task<Result<PurchaseOrder>> Get(Guid orderId)
    {
        var order = _dbContext.PurchaseOrders.Include(o => o.Items).FirstOrDefault(o => o.Id == orderId);
        if (order == null)
            return Result<PurchaseOrder>.Failed("Order not found.");

        return Result<PurchaseOrder>.Success(order, "order fetched successfully.");
    }

    public async Task<Result<PurchaseOrder>> Submit(Guid orderId)
    {
        var order = await _dbContext.PurchaseOrders.FirstOrDefaultAsync(o =>
            o.Id == orderId && o.UserId == _currentUserId);
        if (order == null)
            return Result<PurchaseOrder>.Failed("Order not found.");

        //check user orders
        var today = DateTime.Now.Date;
        if (await _dbContext.PurchaseOrders.CountAsync(o => o.UserId == _currentUserId
                                                            && o.CreationTime.Date == today
                                                            && o.Status == OrderStatus.Submitted) > 10)
            return Result<PurchaseOrder>.Failed("Maximum submitted order count per day is 10.");

        order.Status = OrderStatus.Submitted;

        //save db
        await _dbContext.SaveChangesAsync();

        return Result<PurchaseOrder>.Success(order, "Order submitted successfully.");
    }
}