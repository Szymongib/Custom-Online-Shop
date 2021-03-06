﻿// <auto-generated />
using ApplicationInfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ApplicationInfrastructure.Migrations
{
    [DbContext(typeof(OnlineShopDbContext))]
    partial class OnlineShopDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicationCore.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("ApplicationCore.Models.CartEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CartID");

                    b.Property<int>("ProductID");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("CartID");

                    b.ToTable("CartEntries");
                });

            modelBuilder.Entity("ApplicationCore.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Canceled");

                    b.Property<bool>("Confirmed");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Note");

                    b.Property<bool>("Paid");

                    b.Property<bool>("Shipped");

                    b.Property<int>("ShippingInfoId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ShippingInfoId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ApplicationCore.Models.OrderEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrderID");

                    b.Property<int>("ProductID");

                    b.Property<string>("ProductName");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderEntries");
                });

            modelBuilder.Entity("ApplicationCore.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("FullDescription");

                    b.Property<string>("ImgPath");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<string>("ShortDescription");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ApplicationCore.Models.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.HasKey("Id");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("ApplicationCore.Models.ProductReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorID");

                    b.Property<string>("AuthorUserName");

                    b.Property<string>("Description");

                    b.Property<DateTime>("PostDate");

                    b.Property<int>("ProductID");

                    b.Property<int>("Rating");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductReviews");
                });

            modelBuilder.Entity("ApplicationCore.Models.ShippingInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("UserId");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("ShippingInfo");
                });

            modelBuilder.Entity("ApplicationCore.Models.CartEntry", b =>
                {
                    b.HasOne("ApplicationCore.Models.Cart")
                        .WithMany("CartEntries")
                        .HasForeignKey("CartID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplicationCore.Models.Order", b =>
                {
                    b.HasOne("ApplicationCore.Models.ShippingInfo", "ShippingInfo")
                        .WithMany()
                        .HasForeignKey("ShippingInfoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplicationCore.Models.OrderEntry", b =>
                {
                    b.HasOne("ApplicationCore.Models.Order", "Order")
                        .WithMany("OrderEntries")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApplicationCore.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplicationCore.Models.Product", b =>
                {
                    b.HasOne("ApplicationCore.Models.ProductCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApplicationCore.Models.ProductReview", b =>
                {
                    b.HasOne("ApplicationCore.Models.Product", "Product")
                        .WithMany("ProductReviews")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
