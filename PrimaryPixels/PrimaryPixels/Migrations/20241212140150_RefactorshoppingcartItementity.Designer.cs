﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrimaryPixels.Data;

#nullable disable

namespace PrimaryPixels.Migrations
{
    [DbContext(typeof(PrimaryPixelsContext))]
    [Migration("20241212140150_RefactorshoppingcartItementity")]
    partial class RefactorshoppingcartItementity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PrimaryPixels.Models.Order.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("OrderDate")
                        .HasColumnType("date");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PrimaryPixels.Models.Order.OrderDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("UnitPrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("PrimaryPixels.Models.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Availability")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<int>("TotalSold")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasDiscriminator<string>("ProductType").HasValue("Product");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("PrimaryPixels.Models.ShoppingCartItem.ShoppingCartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ShoppingCartItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ProductId = 4,
                            Quantity = 2,
                            UserId = 3
                        },
                        new
                        {
                            Id = 2,
                            ProductId = 2,
                            Quantity = 4,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            ProductId = 2,
                            Quantity = 1,
                            UserId = 2
                        },
                        new
                        {
                            Id = 4,
                            ProductId = 3,
                            Quantity = 2,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("PrimaryPixels.Models.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "joe@gmail.com",
                            Password = "Joe123",
                            Username = "Joe88"
                        },
                        new
                        {
                            Id = 2,
                            Email = "charles@gmail.com",
                            Password = "charlie10",
                            Username = "Charles11"
                        },
                        new
                        {
                            Id = 3,
                            Email = "maxiking@gmail.com",
                            Password = "maximusminimus",
                            Username = "Maximus"
                        });
                });

            modelBuilder.Entity("PrimaryPixels.Models.Products.Device", b =>
                {
                    b.HasBaseType("PrimaryPixels.Models.Products.Product");

                    b.Property<string>("Cpu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Cpu");

                    b.Property<int>("InternalMemory")
                        .HasColumnType("int")
                        .HasColumnName("InternalMemory");

                    b.Property<int>("Ram")
                        .HasColumnType("int")
                        .HasColumnName("Ram");

                    b.HasDiscriminator().HasValue("Device");
                });

            modelBuilder.Entity("PrimaryPixels.Models.Products.Headphone", b =>
                {
                    b.HasBaseType("PrimaryPixels.Models.Products.Product");

                    b.Property<bool>("Wireless")
                        .HasColumnType("bit")
                        .HasColumnName("Wireless");

                    b.HasDiscriminator().HasValue("Headphone");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Availability = true,
                            Name = "Ultra pro max Headphone 2000",
                            Price = 500,
                            TotalSold = 0,
                            Wireless = false
                        },
                        new
                        {
                            Id = 4,
                            Availability = true,
                            Name = "Ultra pro max Headphone 5000",
                            Price = 1000,
                            TotalSold = 0,
                            Wireless = false
                        },
                        new
                        {
                            Id = 5,
                            Availability = true,
                            Name = "Ultra pro max Headphone 1000",
                            Price = 200,
                            TotalSold = 0,
                            Wireless = true
                        });
                });

            modelBuilder.Entity("PrimaryPixels.Models.Products.Computer", b =>
                {
                    b.HasBaseType("PrimaryPixels.Models.Products.Device");

                    b.Property<bool>("DvdPlayer")
                        .HasColumnType("bit")
                        .HasColumnName("DvdPlayer");

                    b.HasDiscriminator().HasValue("Computer");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Availability = true,
                            Name = "Gaming PC 3510",
                            Price = 100000,
                            TotalSold = 0,
                            Cpu = "I3-6100",
                            InternalMemory = 512,
                            Ram = 8,
                            DvdPlayer = false
                        },
                        new
                        {
                            Id = 3,
                            Availability = true,
                            Name = "Gaming PC 5000",
                            Price = 5000000,
                            TotalSold = 0,
                            Cpu = "I5-8100",
                            InternalMemory = 1024,
                            Ram = 16,
                            DvdPlayer = false
                        });
                });

            modelBuilder.Entity("PrimaryPixels.Models.Products.Phone", b =>
                {
                    b.HasBaseType("PrimaryPixels.Models.Products.Device");

                    b.Property<bool>("CardIndependency")
                        .HasColumnType("bit")
                        .HasColumnName("CardIndependency");

                    b.HasDiscriminator().HasValue("Phone");

                    b.HasData(
                        new
                        {
                            Id = 6,
                            Availability = true,
                            Name = "Redmi A24",
                            Price = 100000,
                            TotalSold = 0,
                            Cpu = "Dimensity 9400",
                            InternalMemory = 128,
                            Ram = 4,
                            CardIndependency = true
                        },
                        new
                        {
                            Id = 7,
                            Availability = true,
                            Name = "iPhone 19",
                            Price = 5000000,
                            TotalSold = 0,
                            Cpu = "Dimensity 11000",
                            InternalMemory = 512,
                            Ram = 16,
                            CardIndependency = false
                        },
                        new
                        {
                            Id = 8,
                            Availability = true,
                            Name = "Redmi A29",
                            Price = 600000,
                            TotalSold = 0,
                            Cpu = "Dimensity 9800",
                            InternalMemory = 256,
                            Ram = 8,
                            CardIndependency = true
                        });
                });

            modelBuilder.Entity("PrimaryPixels.Models.ShoppingCartItem.ShoppingCartItem", b =>
                {
                    b.HasOne("PrimaryPixels.Models.Products.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
