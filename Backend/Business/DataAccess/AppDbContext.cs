using System.Reflection;
using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Business.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Filename=LocalDatabase.db", options =>
        {
            options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        });
        base.OnConfiguring(options);
        // options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
    }

    // public DbSet<User> Users { get; set; }
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public DbSet<PurchaseOrderListItem> PurchaseOrderListItems { get; set; }
}