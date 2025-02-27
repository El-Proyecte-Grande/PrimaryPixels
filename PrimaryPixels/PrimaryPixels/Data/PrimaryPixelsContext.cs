using Microsoft.EntityFrameworkCore;
using PrimaryPixels.Models;
using PrimaryPixels.Models.Order;
using PrimaryPixels.Models.Products;
using PrimaryPixels.Models.ShoppingCartItem;

namespace PrimaryPixels.Data;

public class PrimaryPixelsContext : DbContext
{
    private IConfiguration _configuration;
    public PrimaryPixelsContext(DbContextOptions<PrimaryPixelsContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_configuration["ConnectionString"]);
        }
    }

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
                new Computer { Id = 1, Name = "Gaming PC 3510", Price = 100000, Availability = true, TotalSold = 0, DvdPlayer = false, Cpu = "I3-6100", InternalMemory = 512, Ram = 8, Image="https://s13emagst.akamaized.net/products/71331/71330955/images/res_b75e8c737204f265c382468aca168f75.jpg?width=720&height=720&hash=90CF69FDA4FFA74B09714BE0BBA2A69A"},
                new Computer { Id = 2, Name = "Gaming PC 5000", Price = 5000000, Availability = true, TotalSold = 0, DvdPlayer = false, Cpu = "I5-8100", InternalMemory = 1024, Ram = 16, Image="https://s13emagst.akamaized.net/products/16684/16683989/images/res_45eee4b6571e6820111f638716cf3bf5.jpg?width=720&height=720&hash=1823932085ED244C3F414C27A78312AC"},
                new Computer { Id = 3, Name = "Gamer PC A-I5642", Price = 800000, Availability = true, TotalSold = 3, DvdPlayer = true, Cpu = "i9-14911KF", InternalMemory = 2048, Ram = 16, Image = "https://s13emagst.akamaized.net/products/15188/15187279/images/res_44116bf82dc775127335f7aba3509421.jpg?width=720&height=720&hash=4F2A3C176CD289E8513E4E136C00D283"},
                new Computer { Id = 4, Name = "Gamer PC A-I7689", Price = 1000000, Availability = false, TotalSold = 5, DvdPlayer = true, Cpu = "i9-45500KJ", InternalMemory = 2048, Ram = 32, Image = "https://s13emagst.akamaized.net/products/15188/15187279/images/res_44116bf82dc775127335f7aba3509421.jpg?width=720&height=720&hash=4F2A3C176CD289E8513E4E136C00D283"},
                new Computer { Id = 5, Name = "Gamer PC C-I4567", Price = 1300000, Availability = true, TotalSold = 11, DvdPlayer = false, Cpu = "I6-2300", InternalMemory = 1024, Ram = 32, Image = "https://s13emagst.akamaized.net/products/16684/16683989/images/res_45eee4b6571e6820111f638716cf3bf5.jpg?width=720&height=720&hash=1823932085ED244C3F414C27A78312AC"} 
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
                new Headphone { Id = 6, Name = "Ultra pro HP 5000", Price = 500, Availability = true, TotalSold = 0, Wireless = false, Image="https://s13emagst.akamaized.net/products/78736/78735363/images/res_42c4ab07b083c382ac958c5abde97ccd.jpg?width=720&height=720&hash=AF0FC648A7C459193BB006D07F758597"},
                new Headphone { Id = 7, Name = "DAH6789WH Bluetooth", Price = 20000, Availability = true, TotalSold = 1, Wireless = true, Image = "https://s13emagst.akamaized.net/products/46154/46153291/images/res_3d60cf092b7bb976cb438aeb4788d04f.jpg?width=720&height=720&hash=27C95317E6A8947487149076F5F52C1E"},
                new Headphone { Id = 8, Name = "Kids Headphone 3000", Price = 2000, Availability = false, TotalSold = 1, Wireless = false, Image = "https://s13emagst.akamaized.net/products/78736/78735363/images/res_42c4ab07b083c382ac958c5abde97ccd.jpg?width=720&height=720&hash=AF0FC648A7C459193BB006D07F758597"},
                new Headphone { Id = 9, Name = "Ultra wireless GP", Price = 3000, Availability = true, TotalSold = 9, Wireless = true, Image = "https://s13emagst.akamaized.net/products/34948/34947038/images/res_6aff00e29067abeb44a4172c9518a711.jpg?width=720&height=720&hash=150D280D7245265AAEAB81328D899B73"},
                new Headphone { Id = 10, Name = "Ultra Stereo BT 30", Price = 15000, Availability = true, TotalSold = 0, Wireless = true, Image = "https://s13emagst.akamaized.net/products/46154/46153291/images/res_3d60cf092b7bb976cb438aeb4788d04f.jpg?width=720&height=720&hash=27C95317E6A8947487149076F5F52C1E"}
                );
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.Property(p => p.CardIndependency).HasColumnName("CardIndependency");
            entity.Property(d => d.Cpu).HasColumnName("Cpu");
            entity.Property(d => d.Ram).HasColumnName("Ram");
            entity.Property(d => d.InternalMemory).HasColumnName("InternalMemory");
            entity.HasData(
                new Phone { Id = 11, Name = "Redmi A24", Price = 100000, Availability = true, TotalSold = 0, CardIndependency  = true, Cpu = "Dimensity 9400", InternalMemory = 128, Ram = 4, Image="https://s13emagst.akamaized.net/products/49762/49761516/images/res_a3c8676c0043ba09f646d836b7fc37bb.jpg?width=720&height=720&hash=30109537CDA74ED1D82D5D87E13E5BE1"},
                new Phone { Id = 12, Name = "iPhone 19", Price = 5000000, Availability = true, TotalSold = 0, CardIndependency  = false, Cpu = "Dimensity 11000", InternalMemory = 512, Ram = 16, Image="https://s13emagst.akamaized.net/products/62470/62469084/images/res_119d9158060ffab289ace1eb4fb5f285.jpg?width=720&height=720&hash=2025575EAA3830851B708B6666E37773"},
                new Phone { Id = 13, Name = "Redmi A29", Price = 600000, Availability = true, TotalSold = 0, CardIndependency  = true, Cpu = "Dimensity 9800", InternalMemory = 256, Ram = 8, Image="https://s13emagst.akamaized.net/products/57643/57642529/images/res_77d22d24346a96ca4e3f4e7a9eeea779.jpg?width=720&height=720&hash=61507AB28D44CE94F541B937B7FB239C"},
                new Phone { Id = 14, Name = "Samsung Galaxy E100", Price = 260000, Availability = false, TotalSold = 2, CardIndependency = false, Cpu = "Exynos 4520", InternalMemory = 256, Ram = 8, Image = "https://s13emagst.akamaized.net/products/62470/62469084/images/res_119d9158060ffab289ace1eb4fb5f285.jpg?width=720&height=720&hash=2025575EAA3830851B708B6666E37773"},
                new Phone { Id = 15, Name = "HONOR AZC", Price = 105000, Availability = true, TotalSold = 8, CardIndependency = true, Cpu = "Exynos 3434", InternalMemory = 128, Ram = 16, Image = "https://s13emagst.akamaized.net/products/57643/57642529/images/res_77d22d24346a96ca4e3f4e7a9eeea779.jpg?width=720&height=720&hash=61507AB28D44CE94F541B937B7FB239C"}
                );
        });
    
        modelBuilder.Entity<OrderDetails>()
            .HasOne(od => od.Product)
            .WithMany() 
            .HasForeignKey(od => od.ProductId)
            .IsRequired(false);
    }
}