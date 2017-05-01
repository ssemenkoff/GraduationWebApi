using Microsoft.EntityFrameworkCore;

namespace Core_Server.Models.Data {
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubCategory>()
                .HasOne(p => p.Category);
                //.WithMany(p => p.SubCategories);

            modelBuilder.Entity<Category>()
                .HasOne(p => p.Image);

            modelBuilder.Entity<SubCategory>()
                .HasOne(p => p.Image);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.SubCategory);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithOne(p => p.Product);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.PreviewImage);

            modelBuilder.Entity<Product>()
                .HasMany<OrderProduct>(p => p.Orders)
                .WithOne(p => p.Product);

            modelBuilder.Entity<Order>()
                .HasMany<OrderProduct>(p => p.Products)
                .WithOne(p => p.Order);
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProduct { get; set; }
    }
}