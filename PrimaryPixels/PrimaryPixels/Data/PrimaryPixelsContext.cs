using Microsoft.EntityFrameworkCore;
using PrimaryPixels.Models;
using PrimaryPixels.Models.Order;
using PrimaryPixels.Models.Products;
using PrimaryPixels.Models.ShoppingCartItem;
using PrimaryPixels.Models.Users;

namespace PrimaryPixels.Data;

public class PrimaryPixelsContext : DbContext
{
    public PrimaryPixelsContext(DbContextOptions<PrimaryPixelsContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasDiscriminator<string>("ProductType")
                .HasValue<Headphone>("Headphone")
                .HasValue<Computer>("Computer")
                .HasValue<Phone>("Phone");
        });

        modelBuilder.Entity<Computer>(entity =>
        {
            entity.Property(c => c.DvdPlayer).HasColumnName("DvdPlayer");
            entity.Property(c => c.Cpu).HasColumnName("Cpu");
            entity.Property(c => c.Ram).HasColumnName("Ram");
            entity.Property(c => c.InternalMemory).HasColumnName("InternalMemory");
            entity.HasData(
                new Computer { Id = 2, Name = "Gaming PC 3510", Price = 100000, Availability = true, TotalSold = 0, DvdPlayer = false, Cpu = "I3-6100", InternalMemory = 512, Ram = 8},
                new Computer { Id = 3, Name = "Gaming PC 5000", Price = 5000000, Availability = true, TotalSold = 0, DvdPlayer = false, Cpu = "I5-8100", InternalMemory = 1024, Ram = 16}
                );
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.Property(d => d.Cpu).HasColumnName("Cpu");
            entity.Property(d => d.Ram).HasColumnName("Ram");
            entity.Property(d => d.InternalMemory).HasColumnName("InternalMemory");
        });

        modelBuilder.Entity<Headphone>(entity =>
        {
            entity.Property(h => h.Wireless).HasColumnName("Wireless");
            entity.HasData(
                new Headphone { Id = 1, Name = "Ultra pro max Headphone 2000", Price = 500, Availability = true, TotalSold = 0, Wireless = false},
                new Headphone { Id = 4, Name = "Ultra pro max Headphone 5000", Price = 1000, Availability = true, TotalSold = 0, Wireless = false},
                new Headphone { Id = 5, Name = "Ultra pro max Headphone 1000", Price = 200, Availability = true, TotalSold = 0, Wireless = true}
                );
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.Property(p => p.CardIndependency).HasColumnName("CardIndependency");
            entity.Property(d => d.Cpu).HasColumnName("Cpu");
            entity.Property(d => d.Ram).HasColumnName("Ram");
            entity.Property(d => d.InternalMemory).HasColumnName("InternalMemory");
            entity.HasData(
                new Phone { Id = 6, Name = "Redmi A24", Price = 100000, Availability = true, TotalSold = 0, CardIndependency  = true, Cpu = "Dimensity 9400", InternalMemory = 128, Ram = 4},
                new Phone { Id = 7, Name = "iPhone 19", Price = 5000000, Availability = true, TotalSold = 0, CardIndependency  = false, Cpu = "Dimensity 11000", InternalMemory = 512, Ram = 16},
                new Phone { Id = 8, Name = "Redmi A29", Price = 600000, Availability = true, TotalSold = 0, CardIndependency  = true, Cpu = "Dimensity 9800", InternalMemory = 256, Ram = 8}
                );
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasData(
                new User { Id = 1, Username = "Joe88", Password = "Joe123", Email = "joe@gmail.com" },
                new User { Id = 2, Username = "Charles11", Password = "charlie10", Email = "charles@gmail.com" },
                new User { Id = 3, Username = "Maximus", Password = "maximusminimus", Email = "maxiking@gmail.com" }
            );
        });

        // modelBuilder.Entity<Order>(entity =>
        // {
        //     entity.HasData(
        //         new Order{Address = "Kis Mihály street 6", City = "Túrkeve", UserId = 1, OrderDate = new DateOnly(2024,11,26), Id = 1},
        //         new Order{Address = "Charles street 14", City = "London", UserId = 2, OrderDate = new DateOnly(2024,10,1), Id = 2}
        //     );
        // });
        //
        // modelBuilder.Entity<OrderDetails>(entity =>
        // {
        //     entity.HasData(
        //         new OrderDetails{Id = 1, OrderId = 1, ProductId = 1, Quantity = 3, UnitPrice = 50000},
        //         new OrderDetails{Id = 2, OrderId = 1, ProductId = 3, Quantity = 5, UnitPrice = 750000},
        //         new OrderDetails{Id = 3, OrderId = 2, ProductId = 1, Quantity = 1, UnitPrice = 154000},
        //         new OrderDetails{Id = 4, OrderId = 2, ProductId = 5, Quantity = 3, UnitPrice = 1000000},
        //         new OrderDetails{Id = 5, OrderId = 2, ProductId = 6, Quantity = 1, UnitPrice = 50000}
        //     );
        // });
        
        modelBuilder.Entity<ShoppingCartItem>
        (entity =>
        {
            entity.HasData(
                new ShoppingCartItem() { Id = 1, ProductId = 4, Quantity = 2, UserId = 3 },
                new ShoppingCartItem() { Id = 2, ProductId = 2, Quantity = 4, UserId = 1 },
                new ShoppingCartItem() { Id = 3, ProductId = 2, Quantity = 1, UserId = 2 },
                new ShoppingCartItem() { Id = 4, ProductId = 3, Quantity = 2, UserId = 2 }
            );
        });
    }
}