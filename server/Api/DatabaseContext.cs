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
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Cart> Carts => Set<Cart>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brand>().HasData(
                new Brand { Name = "samsung" },
                new Brand { Name = "iphone" },
                new Brand { Name = "xiaomi" },
                new Brand { Name = "huawei" },
                new Brand { Name = "nokia" },
                new Brand { Name = "lg" },
                new Brand { Name = "sony" },
                new Brand { Name = "motorola" }
            );

            modelBuilder.Entity<Model>().HasData(
                new Model { Name = "galaxy s23", BrandId = 1 },
                new Model { Name = "galaxy note 20", BrandId = 1 },
                new Model { Name = "galaxy z fold 4", BrandId = 1 },
                new Model { Name = "iphone 14", BrandId = 2 },
                new Model { Name = "iphone 13", BrandId = 2 },
                new Model { Name = "iphone 12", BrandId = 2 },
                new Model { Name = "mi 11", BrandId = 3 },
                new Model { Name = "redmi note 10", BrandId = 3 },
                new Model { Name = "poco f4", BrandId = 3 },
                new Model { Name = "p40 pro", BrandId = 4 },
                new Model { Name = "mate 40 pro", BrandId = 4 },
                new Model { Name = "p30 lite", BrandId = 4 },
                new Model { Name = "nokia 8.3", BrandId = 5 },
                new Model { Name = "nokia 7.2", BrandId = 5 },
                new Model { Name = "nokia 5.4", BrandId = 5 },
                new Model { Name = "lg velvet", BrandId = 6 },
                new Model { Name = "lg v60 thinq", BrandId = 6 },
                new Model { Name = "lg g8x thinq", BrandId = 6 },
                new Model { Name = "sony xperia 1 ii", BrandId = 7 },
                new Model { Name = "sony xperia 10 ii", BrandId = 7 },
                new Model { Name = "sony xperia l4", BrandId = 7 },
                new Model { Name = "moto g power", BrandId = 8 },
                new Model { Name = "moto edge 20", BrandId = 8 },
                new Model { Name = "moto e 2020", BrandId = 8 }
            );

            modelBuilder.Entity<Color>().HasData(
                new Color { Name = "black" },
                new Color { Name = "white" },
                new Color { Name = "blue" },
                new Color { Name = "gray" },
                new Color { Name = "gold" }
            );

        }
    }
}
