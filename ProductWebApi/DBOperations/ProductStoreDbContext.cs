using Microsoft.EntityFrameworkCore;
using ProductWebApi.Entities;

namespace ProductWebApi.DBOperations
{
    public class ProductStoreDbContext : DbContext
    {
        public ProductStoreDbContext(DbContextOptions<ProductStoreDbContext> options) : base(options)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasKey(a => a.CategoryId);
            modelBuilder.Entity<Product>()
                .HasKey(b => b.Id);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
