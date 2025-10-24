using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Item> Items => Set<Item>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<StockMovement> StockMovements => Set<StockMovement>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Item>()
             .HasIndex(i => i.Code)
             .IsUnique(); // avoid duplicate codes

            b.Entity<Item>()
             .HasOne(i => i.Category)
             .WithMany(c => c.Items)
             .HasForeignKey(i => i.CategoryId)
             .OnDelete(DeleteBehavior.SetNull);

            b.Entity<Item>()
             .HasOne(i => i.Supplier)
             .WithMany(s => s.Items)
             .HasForeignKey(i => i.SupplierId)
             .OnDelete(DeleteBehavior.SetNull);

            b.Entity<Item>().Property(i => i.UnitPrice).HasPrecision(18, 2);
        }
    }
}
