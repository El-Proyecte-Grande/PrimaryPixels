using Microsoft.EntityFrameworkCore;
using PrimaryPixels.Models;
using PrimaryPixels.Models.Order;
using PrimaryPixels.Models.Products;
using PrimaryPixels.Models.ShoppingCartItem;

namespace PrimaryPixels.Data;

public class PrimaryPixelsContext : DbContext
{
    public PrimaryPixelsContext(DbContextOptions<PrimaryPixelsContext> options) : base(options)
    {
    }
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
                new Computer { Id = 2, Name = "Gaming PC 3510", Price = 100000, Availability = true, TotalSold = 0, DvdPlayer = false, Cpu = "I3-6100", InternalMemory = 512, Ram = 8, Image="https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_98587778/mobile_786_587_png/X-X-GAMER-I3228-Gamer-PC-%28Core-i5-16GB-480-GB-SSD---2-TB-HDD-RX6750XT-12GB-NoOS%29"},
                new Computer { Id = 3, Name = "Gaming PC 5000", Price = 5000000, Availability = true, TotalSold = 0, DvdPlayer = false, Cpu = "I5-8100", InternalMemory = 1024, Ram = 16, Image="https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_143744896/mobile_786_587_png/SHARKGAMING-RGBeast-R900-SGRGBR900-33-4090-Gamer-PC-%28Ryzen9-32GB-2x1024-GB-SSD-Win11H%29"}
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
                new Headphone { Id = 1, Name = "Ultra pro max Headphone 2000", Price = 500, Availability = true, TotalSold = 0, Wireless = false, Image="https://cdn.lifehack.org/wp-content/uploads/2014/12/28.jpg"}
                );
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.Property(p => p.CardIndependency).HasColumnName("CardIndependency");
            entity.Property(d => d.Cpu).HasColumnName("Cpu");
            entity.Property(d => d.Ram).HasColumnName("Ram");
            entity.Property(d => d.InternalMemory).HasColumnName("InternalMemory");
            entity.HasData(
                new Phone { Id = 6, Name = "Redmi A24", Price = 100000, Availability = true, TotalSold = 0, CardIndependency  = true, Cpu = "Dimensity 9400", InternalMemory = 128, Ram = 4, Image="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSELHPTLjLUf8sBWPzXg7bTDdG1nClBF5Kc4A&s"},
                new Phone { Id = 7, Name = "iPhone 19", Price = 5000000, Availability = true, TotalSold = 0, CardIndependency  = false, Cpu = "Dimensity 11000", InternalMemory = 512, Ram = 16, Image="https://www.tecnosell.com/media/catalog/product/cache/60c31028333b516fd0f8945d994bb7aa/b/l/blu1_2_7_1.jpg"},
                new Phone { Id = 8, Name = "Redmi A29", Price = 600000, Availability = true, TotalSold = 0, CardIndependency  = true, Cpu = "Dimensity 9800", InternalMemory = 256, Ram = 8, Image="https://cdn.tmobile.com/content/dam/t-mobile/en-p/cell-phones/apple/Apple-iPhone-15-Plus/Pink/Apple-iPhone-15-Plus-Pink-thumbnail.png"}
                );
        });

       
        // modelBuilder.Entity<Order>(entity =>
        // {
        //     entity.HasData(
        //         new Order{Address = "Kis Mihály street 6", City = "Túrkeve", UserId = 1, OrderDate = new DateOnly(2024,11,26), Id = 1},
        //         new Order{Address = "Charles street 14", City = "London", UserId = 2, OrderDate = new DateOnly(2024,10,1), Id = 2}
        //     );
        // });

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
        //
        // modelBuilder.Entity<ShoppingCartItem>(entity =>
        // {
        //     entity.HasData(
        //         new ShoppingCartItem() { Id = 1, ProductId = 4, Quantity = 2, UserId = 3 },
        //         new ShoppingCartItem() { Id = 2, ProductId = 2, Quantity = 4, UserId = 1 },
        //         new ShoppingCartItem() { Id = 3, ProductId = 2, Quantity = 1, UserId = 2 },
        //         new ShoppingCartItem() { Id = 4, ProductId = 3, Quantity = 2, UserId = 2 }
        //     );
        // });
    }
}