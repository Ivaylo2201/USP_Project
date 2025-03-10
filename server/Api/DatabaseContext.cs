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
                new Brand { Id = 1, Name = "samsung" },
                new Brand { Id = 2, Name = "iphone" },
                new Brand { Id = 3, Name = "xiaomi" },
                new Brand { Id = 4, Name = "huawei" },
                new Brand { Id = 5, Name = "nokia" },
                new Brand { Id = 6, Name = "lg" },
                new Brand { Id = 7, Name = "sony" },
                new Brand { Id = 8, Name = "motorola" }
            );

            modelBuilder.Entity<Model>().HasData(
                new Model { Id = 1, Name = "galaxy s23", BrandId = 1 },
                new Model { Id = 2, Name = "galaxy note 20", BrandId = 1 },
                new Model { Id = 3, Name = "galaxy z fold 4", BrandId = 1 },
                new Model { Id = 4, Name = "iphone 14", BrandId = 2 },
                new Model { Id = 5, Name = "iphone 13", BrandId = 2 },
                new Model { Id = 6, Name = "iphone 12", BrandId = 2 },
                new Model { Id = 7, Name = "mi 11", BrandId = 3 },
                new Model { Id = 8, Name = "redmi note 10", BrandId = 3 },
                new Model { Id = 9, Name = "poco f4", BrandId = 3 },
                new Model { Id = 10, Name = "p40 pro", BrandId = 4 },
                new Model { Id = 11, Name = "mate 40 pro", BrandId = 4 },
                new Model { Id = 12, Name = "p30 lite", BrandId = 4 },
                new Model { Id = 13, Name = "nokia 8.3", BrandId = 5 },
                new Model { Id = 14, Name = "nokia 7.2", BrandId = 5 },
                new Model { Id = 15, Name = "nokia 5.4", BrandId = 5 },
                new Model { Id = 16, Name = "lg velvet", BrandId = 6 },
                new Model { Id = 17, Name = "lg v60 thinq", BrandId = 6 },
                new Model { Id = 18, Name = "lg g8x thinq", BrandId = 6 },
                new Model { Id = 19, Name = "sony xperia 1 ii", BrandId = 7 },
                new Model { Id = 20, Name = "sony xperia 10 ii", BrandId = 7 },
                new Model { Id = 21, Name = "sony xperia l4", BrandId = 7 },
                new Model { Id = 22, Name = "moto g power", BrandId = 8 },
                new Model { Id = 23, Name = "moto edge 20", BrandId = 8 },
                new Model { Id = 24, Name = "moto e 2020", BrandId = 8 }
            );

            modelBuilder.Entity<Color>().HasData(
                new Color { Id = 1, Name = "black" },
                new Color { Id = 2, Name = "white" },
                new Color { Id = 3, Name = "blue" },
                new Color { Id = 4, Name = "gray" },
                new Color { Id = 5, Name = "gold" }
            );

            modelBuilder.Entity<Phone>().HasData(
                new Phone { Id = 1, BrandId = 1, ModelId = 1, ColorId = 1, Price = 500 },
                new Phone { Id = 2, BrandId = 1, ModelId = 2, ColorId = 2, Price = 600 },
                new Phone { Id = 3, BrandId = 1, ModelId = 3, ColorId = 3, Price = 1200 },
                new Phone { Id = 4, BrandId = 2, ModelId = 4, ColorId = 4, Price = 1000 },
                new Phone { Id = 5, BrandId = 2, ModelId = 5, ColorId = 5, Price = 800 }
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
