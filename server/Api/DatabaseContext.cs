using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Color> Colors => Set<Color>();
        public DbSet<Model> Models => Set<Model>();
        public DbSet<Phone> Phones => Set<Phone>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<Item> Items => Set<Item>();

        protected void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Samsung" },
                new Brand { Id = 2, Name = "Apple" },
                new Brand { Id = 3, Name = "Xiaomi" }
            );

            modelBuilder.Entity<Model>().HasData(
                new Model { Id = 1, Name = "Galaxy S23", BrandId = 1 },
                new Model { Id = 2, Name = "IPhone 14", BrandId = 2 },
                new Model { Id = 3, Name = "Redmi Note 13 Pro", BrandId = 3 }
            );

            modelBuilder.Entity<Color>().HasData(
                new Color { Id = 1, Name = "Black" },
                new Color { Id = 2, Name = "White" },
                new Color { Id = 3, Name = "Blue" },
                new Color { Id = 4, Name = "Gray" },
                new Color { Id = 5, Name = "Gold" },
                new Color { Id = 6, Name = "Pink" }
            );

            modelBuilder.Entity<Phone>().HasData(
                new Phone { Id = 1, BrandId = 1, ModelId = 1, ColorId = 6, Price = 750, ImagePath = "/uploads/samsung-galaxy-s23.jpg" },
                new Phone { Id = 2, BrandId = 2, ModelId = 2, ColorId = 1, Price = 1500, ImagePath = "/uploads/apple-iphone-14.jpg" },
                new Phone { Id = 3, BrandId = 3, ModelId = 3, ColorId = 1, Price = 300, ImagePath = "/uploads/xiaomi-redminote-13-pro.jpg" }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId);

            modelBuilder.Entity<Phone>().HasMany(p => p.LikedBy).WithMany(u => u.LikedPhones);
            this.Seed(modelBuilder);
        }
    }
}
