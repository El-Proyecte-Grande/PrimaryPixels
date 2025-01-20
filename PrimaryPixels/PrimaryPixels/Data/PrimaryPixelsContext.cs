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
                new Computer { Id = 1, Name = "Gaming PC 3510", Price = 100000, Availability = true, TotalSold = 0, DvdPlayer = false, Cpu = "I3-6100", InternalMemory = 512, Ram = 8, Image="https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_98587778/mobile_786_587_png/X-X-GAMER-I3228-Gamer-PC-%28Core-i5-16GB-480-GB-SSD---2-TB-HDD-RX6750XT-12GB-NoOS%29"},
                new Computer { Id = 2, Name = "Gaming PC 5000", Price = 5000000, Availability = true, TotalSold = 0, DvdPlayer = false, Cpu = "I5-8100", InternalMemory = 1024, Ram = 16, Image="https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_143744896/mobile_786_587_png/SHARKGAMING-RGBeast-R900-SGRGBR900-33-4090-Gamer-PC-%28Ryzen9-32GB-2x1024-GB-SSD-Win11H%29"},
                new Computer { Id = 3, Name = "Gamer PC A-I5642", Price = 800000, Availability = true, TotalSold = 3, DvdPlayer = true, Cpu = "i9-14911KF", InternalMemory = 2048, Ram = 16, Image = "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_146904591?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402"},
                new Computer { Id = 4, Name = "Gamer PC A-I7689", Price = 1000000, Availability = false, TotalSold = 5, DvdPlayer = true, Cpu = "i9-45500KJ", InternalMemory = 2048, Ram = 32, Image = "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_98587780?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402"},
                new Computer { Id = 5, Name = "Gamer PC C-I4567", Price = 1300000, Availability = true, TotalSold = 11, DvdPlayer = false, Cpu = "I6-2300", InternalMemory = 1024, Ram = 32, Image = "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_138935428?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402"} 
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
                new Headphone { Id = 6, Name = "Ultra pro max Headphone 2000", Price = 500, Availability = true, TotalSold = 0, Wireless = false, Image="https://cdn.lifehack.org/wp-content/uploads/2014/12/28.jpg"},
                new Headphone { Id = 7, Name = "DAH6789WH Bluetooth Headphone", Price = 20000, Availability = true, TotalSold = 1, Wireless = true, Image = "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_135532150?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402"},
                new Headphone { Id = 8, Name = "Kids Headphone 3000", Price = 2000, Availability = false, TotalSold = 1, Wireless = false, Image = "https://assets.mmsrg.com/isr/166325/c1/-/pixelboxx-mss-81231790?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402"},
                new Headphone { Id = 9, Name = "Ultra wireless Headphone", Price = 3000, Availability = true, TotalSold = 9, Wireless = true, Image = "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_77379180?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402"},
                new Headphone { Id = 10, Name = "Ultra Stereo BT Headset 300", Price = 15000, Availability = true, TotalSold = 0, Wireless = true, Image = "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_140559725?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402"}
                );
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.Property(p => p.CardIndependency).HasColumnName("CardIndependency");
            entity.Property(d => d.Cpu).HasColumnName("Cpu");
            entity.Property(d => d.Ram).HasColumnName("Ram");
            entity.Property(d => d.InternalMemory).HasColumnName("InternalMemory");
            entity.HasData(
                new Phone { Id = 11, Name = "Redmi A24", Price = 100000, Availability = true, TotalSold = 0, CardIndependency  = true, Cpu = "Dimensity 9400", InternalMemory = 128, Ram = 4, Image="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSELHPTLjLUf8sBWPzXg7bTDdG1nClBF5Kc4A&s"},
                new Phone { Id = 12, Name = "iPhone 19", Price = 5000000, Availability = true, TotalSold = 0, CardIndependency  = false, Cpu = "Dimensity 11000", InternalMemory = 512, Ram = 16, Image="https://www.tecnosell.com/media/catalog/product/cache/60c31028333b516fd0f8945d994bb7aa/b/l/blu1_2_7_1.jpg"},
                new Phone { Id = 13, Name = "Redmi A29", Price = 600000, Availability = true, TotalSold = 0, CardIndependency  = true, Cpu = "Dimensity 9800", InternalMemory = 256, Ram = 8, Image="https://cdn.tmobile.com/content/dam/t-mobile/en-p/cell-phones/apple/Apple-iPhone-15-Plus/Pink/Apple-iPhone-15-Plus-Pink-thumbnail.png"},
                new Phone { Id = 14, Name = "Samsung Galaxy E100", Price = 260000, Availability = false, TotalSold = 2, CardIndependency = false, Cpu = "Exynos 4520", InternalMemory = 256, Ram = 8, Image = "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_137998148?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402"},
                new Phone { Id = 15, Name = "HONOR AZC", Price = 105000, Availability = true, TotalSold = 8, CardIndependency = true, Cpu = "Exynos 3434", InternalMemory = 128, Ram = 16, Image = "https://assets.mmsrg.com/isr/166325/c1/-/ASSET_MMS_144574340?x=536&y=402&format=jpg&quality=80&sp=yes&strip=yes&trim&ex=536&ey=402&align=center&resizesource&unsharp=1.5x1+0.7+0.02&cox=0&coy=0&cdx=536&cdy=402"}
                );
        });

    }
}