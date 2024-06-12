using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Order.Infras.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
        if (context.Orders.Count() == 0) await SeedAsync(context);
    }
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedCustomerAsync(context);
        await SeedProductAsync(context);
        await SeedOrdersWithItemsAsync(context);

    }

    public static async Task SeedCustomerAsync(ApplicationDbContext context)
    {
        await context.Customers.AddRangeAsync(InitialData.Customers);
        await context.SaveChangesAsync();
    }

    public static async Task SeedProductAsync(ApplicationDbContext context)
    {
        await context.Products.AddRangeAsync(InitialData.Products);
        await context.SaveChangesAsync();
    }
    public static async Task SeedOrdersWithItemsAsync(ApplicationDbContext context)
    {
        await context.Orders.AddRangeAsync(InitialData.OrdersWithItems);
        await context.SaveChangesAsync();
    }
}