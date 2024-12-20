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
    [Migration("20241125132405_InitialCreate")]
    partial class InitialCreate
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

                    b.HasIndex("UserId");

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

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("PrimaryPixels.Models.Product", b =>
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

                    b.Property<int>("Sold")
                        .HasColumnType("int");

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
                });

            modelBuilder.Entity("PrimaryPixels.Models.User", b =>
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
                });

            modelBuilder.Entity("PrimaryPixels.Models.Device", b =>
                {
                    b.HasBaseType("PrimaryPixels.Models.Product");

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

            modelBuilder.Entity("PrimaryPixels.Models.Headphone", b =>
                {
                    b.HasBaseType("PrimaryPixels.Models.Product");

                    b.Property<bool>("Wireless")
                        .HasColumnType("bit")
                        .HasColumnName("Wireless");

                    b.HasDiscriminator().HasValue("Headphone");
                });

            modelBuilder.Entity("PrimaryPixels.Models.Computer", b =>
                {
                    b.HasBaseType("PrimaryPixels.Models.Device");

                    b.Property<bool>("DvdPlayer")
                        .HasColumnType("bit")
                        .HasColumnName("DvdPlayer");

                    b.HasDiscriminator().HasValue("Computer");
                });

            modelBuilder.Entity("PrimaryPixels.Models.Phone", b =>
                {
                    b.HasBaseType("PrimaryPixels.Models.Device");

                    b.Property<bool>("CardIndependency")
                        .HasColumnType("bit")
                        .HasColumnName("CardIndependency");

                    b.HasDiscriminator().HasValue("Phone");
                });

            modelBuilder.Entity("PrimaryPixels.Models.Order.Order", b =>
                {
                    b.HasOne("PrimaryPixels.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PrimaryPixels.Models.Order.OrderDetails", b =>
                {
                    b.HasOne("PrimaryPixels.Models.Order.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrimaryPixels.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PrimaryPixels.Models.ShoppingCartItem.ShoppingCartItem", b =>
                {
                    b.HasOne("PrimaryPixels.Models.Product", "Product")
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
