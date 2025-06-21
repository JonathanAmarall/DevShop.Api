using DevShop.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DevShop.Api.Data
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DevShopDbContext>();
            await context.Database.MigrateAsync();

            if (!context.Products.Any())
            {
                await SeedDataAsync(context);
            }
        }

        private static async Task SeedDataAsync(DevShopDbContext context)
        {
            var products = new[]
            {
                new Product("Laptop Dell Inspiron", "Laptop Dell Inspiron 15 polegadas com processador Intel i5", 2999.99m, 10, "LAP001"),
                new Product("Mouse Wireless", "Mouse wireless com sensor óptico de alta precisão", 89.90m, 50, "MOU001"),
                new Product("Teclado Mecânico", "Teclado mecânico com switches Cherry MX Blue", 299.90m, 25, "TEC001"),
                new Product("Monitor 24\"", "Monitor LED 24 polegadas Full HD", 599.90m, 15, "MON001"),
                new Product("Headset Gamer", "Headset gamer com microfone e som surround", 199.90m, 30, "HEA001")
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}