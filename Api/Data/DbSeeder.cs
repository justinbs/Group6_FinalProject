using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        // Only seed if empty to avoid duplicates
        var needsCategories = !await db.Categories.AnyAsync();
        var needsSuppliers = !await db.Suppliers.AnyAsync();
        var needsItems = !await db.Items.AnyAsync();

        if (needsCategories)
        {
            db.Categories.AddRange(
                new Category { Name = "Electronics" },
                new Category { Name = "Office" },
                new Category { Name = "Grocery" }
            );
        }

        if (needsSuppliers)
        {
            db.Suppliers.AddRange(
                new Supplier { Name = "Acme Corp", Contact = "acme@example.com" },
                new Supplier { Name = "Global Inc", Contact = "global@example.com" }
            );
        }

        if (needsCategories || needsSuppliers)
            await db.SaveChangesAsync();

        if (needsItems)
        {
            var catElec = await db.Categories.FirstAsync(c => c.Name == "Electronics");
            var catOff = await db.Categories.FirstAsync(c => c.Name == "Office");
            var supA = await db.Suppliers.FirstAsync(s => s.Name == "Acme Corp");
            var supG = await db.Suppliers.FirstAsync(s => s.Name == "Global Inc");

            db.Items.AddRange(
                new Item { Name = "Mouse", Brand = "Logi", CategoryId = catElec.Id, SupplierId = supA.Id, UnitPrice = 450, Quantity = 10 },
                new Item { Name = "Keyboard", Brand = "KeyX", CategoryId = catElec.Id, SupplierId = supA.Id, UnitPrice = 1200, Quantity = 5 },
                new Item { Name = "Bond Paper A4", Brand = "PaperPro", CategoryId = catOff.Id, SupplierId = supG.Id, UnitPrice = 350, Quantity = 30 }
            );
            await db.SaveChangesAsync();
        }
    }
}
