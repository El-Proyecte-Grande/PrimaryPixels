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
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.Property(p => p.CardIndependency).HasColumnName("CardIndependency");
            entity.Property(d => d.Cpu).HasColumnName("Cpu");
            entity.Property(d => d.Ram).HasColumnName("Ram");
            entity.Property(d => d.InternalMemory).HasColumnName("InternalMemory");
        });
    }
}