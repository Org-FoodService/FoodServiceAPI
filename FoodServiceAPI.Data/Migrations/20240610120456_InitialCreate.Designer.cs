﻿// <auto-generated />
using System;
using FoodServiceAPI.Data.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodServiceAPI.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240610120456_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FoodService.Models.Auth.Role.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("CanAccessFinancialResources")
                        .HasColumnType("bit");

                    b.Property<bool>("CanAddEmployee")
                        .HasColumnType("bit");

                    b.Property<bool>("CanAddProduct")
                        .HasColumnType("bit");

                    b.Property<bool>("CanChangePrice")
                        .HasColumnType("bit");

                    b.Property<bool>("CanControlStock")
                        .HasColumnType("bit");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("FoodService.Models.Auth.User.UserBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CpfCnpj")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("UserBase");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("FoodService.Models.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFresh")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Ingredient");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Fresh and ripe, our tomatoes are harvested at the peak of perfection, ensuring unmatched flavor and quality.",
                            ExpirationDate = new DateTime(2024, 6, 17, 9, 4, 55, 425, DateTimeKind.Local).AddTicks(5603),
                            Image = "https://i.imgur.com/dNT5NsS.jpg",
                            IsFresh = true,
                            Name = "Tomato",
                            ShortDescription = "Fresh Tomato",
                            StockQuantity = 100
                        },
                        new
                        {
                            Id = 2,
                            Description = "Our lettuces are carefully grown, offering a crisp texture and a light flavor that perfectly complements any salad.",
                            ExpirationDate = new DateTime(2024, 6, 15, 9, 4, 55, 425, DateTimeKind.Local).AddTicks(5621),
                            Image = "https://i.imgur.com/dNT5NsS.jpg",
                            IsFresh = true,
                            Name = "Lettuce",
                            ShortDescription = "Crisp Lettuce",
                            StockQuantity = 50
                        },
                        new
                        {
                            Id = 3,
                            Description = "Our chicken breasts are boneless and carefully prepared to ensure tender, juicy meat, perfect for a variety of dishes.",
                            ExpirationDate = new DateTime(2024, 6, 13, 9, 4, 55, 425, DateTimeKind.Local).AddTicks(5623),
                            Image = "https://i.imgur.com/dNT5NsS.jpg",
                            IsFresh = true,
                            Name = "Chicken Breast",
                            ShortDescription = "Boneless Chicken Breast",
                            StockQuantity = 30
                        },
                        new
                        {
                            Id = 4,
                            Description = "Our cheddar cheese is aged with care to develop its rich, creamy flavor, adding an irresistible touch to any dish.",
                            ExpirationDate = new DateTime(2024, 6, 20, 9, 4, 55, 425, DateTimeKind.Local).AddTicks(5625),
                            Image = "https://i.imgur.com/dNT5NsS.jpg",
                            IsFresh = true,
                            Name = "Cheese",
                            ShortDescription = "Aged Cheddar Cheese",
                            StockQuantity = 40
                        },
                        new
                        {
                            Id = 5,
                            Description = "Our fresh onions are hand-selected to ensure consistent quality and flavor, adding robust, aromatic taste to any dish.",
                            ExpirationDate = new DateTime(2024, 6, 17, 9, 4, 55, 425, DateTimeKind.Local).AddTicks(5627),
                            Image = "https://i.imgur.com/dNT5NsS.jpg",
                            IsFresh = true,
                            Name = "Onion",
                            ShortDescription = "Fresh Onion",
                            StockQuantity = 60
                        },
                        new
                        {
                            Id = 6,
                            Description = "Our fresh lemons are harvested at their peak of freshness, offering a citrusy, refreshing flavor that elevates any beverage or dish.",
                            ExpirationDate = new DateTime(2024, 6, 20, 9, 4, 55, 425, DateTimeKind.Local).AddTicks(5629),
                            Image = "https://i.imgur.com/dNT5NsS.jpg",
                            IsFresh = true,
                            Name = "Lemon",
                            ShortDescription = "Fresh Lemon",
                            StockQuantity = 30
                        });
                });

            modelBuilder.Entity("FoodService.Models.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int?>("TableId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("TableId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("FoodService.Models.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("FoodService.Models.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Active = true,
                            Brand = "Chef's Special",
                            Description = "Our tomato soup is made with the finest fresh tomatoes, seasoned with herbs and spices for a rich, comforting flavor.",
                            Image = "https://i.imgur.com/aHzcc5Q.jpg",
                            Name = "Tomato Soup",
                            Price = 5.9900000000000002,
                            ShortDescription = "Delicious tomato soup",
                            Type = 3
                        },
                        new
                        {
                            Id = 2,
                            Active = true,
                            Brand = "Healthy Kitchen",
                            Description = "Our chicken salad is healthy and delicious, featuring tender chicken breast, crisp lettuce, and fresh vegetables, tossed in a tangy dressing.",
                            Image = "https://i.imgur.com/2iiBEfP.jpg",
                            Name = "Chicken Salad",
                            Price = 8.4900000000000002,
                            ShortDescription = "Healthy chicken salad",
                            Type = 3
                        },
                        new
                        {
                            Id = 3,
                            Active = true,
                            Brand = "Fresh Drinks",
                            Description = "Our lemonade is made with freshly squeezed lemons, pure cane sugar, and filtered water, creating a refreshing beverage that's perfect for any occasion.",
                            Image = "https://i.imgur.com/NFpjHQD.jpg",
                            Name = "Lemonade",
                            Price = 2.9900000000000002,
                            ShortDescription = "Refreshing lemonade",
                            Type = 2
                        },
                        new
                        {
                            Id = 4,
                            Active = true,
                            Brand = "Burger House",
                            Description = "Our classic cheeseburger features a juicy beef patty, melted cheddar cheese, crisp lettuce, ripe tomatoes, onions, and pickles, all served on a toasted bun.",
                            Image = "https://i.imgur.com/dNT5NsS.jpg",
                            Name = "Cheeseburger",
                            Price = 7.9900000000000002,
                            ShortDescription = "Classic cheeseburger",
                            Type = 3
                        },
                        new
                        {
                            Id = 5,
                            Active = true,
                            Brand = "Snack Corner",
                            Description = "Our crispy onion rings are made with fresh onions, coated in a seasoned batter, and fried to golden perfection, creating a delicious side dish or snack.",
                            Image = "https://i.imgur.com/ta6xouW.jpg",
                            Name = "Onion Rings",
                            Price = 3.4900000000000002,
                            ShortDescription = "Crispy onion rings",
                            Type = 3
                        });
                });

            modelBuilder.Entity("FoodService.Models.Entities.ProductIngredient", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("ProductIngredient");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            IngredientId = 1
                        },
                        new
                        {
                            ProductId = 1,
                            IngredientId = 2
                        },
                        new
                        {
                            ProductId = 1,
                            IngredientId = 4
                        },
                        new
                        {
                            ProductId = 2,
                            IngredientId = 2
                        },
                        new
                        {
                            ProductId = 2,
                            IngredientId = 3
                        },
                        new
                        {
                            ProductId = 3,
                            IngredientId = 6
                        },
                        new
                        {
                            ProductId = 4,
                            IngredientId = 1
                        },
                        new
                        {
                            ProductId = 4,
                            IngredientId = 2
                        },
                        new
                        {
                            ProductId = 4,
                            IngredientId = 4
                        },
                        new
                        {
                            ProductId = 5,
                            IngredientId = 5
                        });
                });

            modelBuilder.Entity("FoodService.Models.Entities.SiteSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BackgroundColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DangerColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DarkColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GreenColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Icon")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PrimaryColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondaryColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SuccessColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TertiaryColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SiteSettings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BackgroundColor = "#fffaf3",
                            DangerColor = "#8E291F",
                            DarkColor = "#412D2C",
                            GreenColor = "#376B4C",
                            LastUpdate = new DateTime(2024, 6, 10, 9, 4, 55, 425, DateTimeKind.Local).AddTicks(6057),
                            PrimaryColor = "#AA2E26",
                            SecondaryColor = "#FB9F3A",
                            ServiceName = "FoodService",
                            SuccessColor = "#02EB62",
                            TertiaryColor = "#2CAB61"
                        });
                });

            modelBuilder.Entity("FoodService.Models.Entities.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("FoodService.Models.Auth.User.ClientUser", b =>
                {
                    b.HasBaseType("FoodService.Models.Auth.User.UserBase");

                    b.HasDiscriminator().HasValue("ClientUser");
                });

            modelBuilder.Entity("FoodService.Models.Auth.User.EmployeeUser", b =>
                {
                    b.HasBaseType("FoodService.Models.Auth.User.UserBase");

                    b.HasDiscriminator().HasValue("EmployeeUser");
                });

            modelBuilder.Entity("FoodService.Models.Entities.Order", b =>
                {
                    b.HasOne("FoodService.Models.Entities.Table", null)
                        .WithMany("Orders")
                        .HasForeignKey("TableId");

                    b.HasOne("FoodService.Models.Auth.User.ClientUser", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodService.Models.Entities.OrderItem", b =>
                {
                    b.HasOne("FoodService.Models.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodService.Models.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FoodService.Models.Entities.ProductIngredient", b =>
                {
                    b.HasOne("FoodService.Models.Entities.Ingredient", "Ingredient")
                        .WithMany("ProductIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodService.Models.Entities.Product", "Product")
                        .WithMany("ProductIngredients")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("FoodService.Models.Auth.Role.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("FoodService.Models.Auth.User.UserBase", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("FoodService.Models.Auth.User.UserBase", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("FoodService.Models.Auth.Role.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodService.Models.Auth.User.UserBase", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("FoodService.Models.Auth.User.UserBase", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodService.Models.Entities.Ingredient", b =>
                {
                    b.Navigation("ProductIngredients");
                });

            modelBuilder.Entity("FoodService.Models.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("FoodService.Models.Entities.Product", b =>
                {
                    b.Navigation("ProductIngredients");
                });

            modelBuilder.Entity("FoodService.Models.Entities.Table", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("FoodService.Models.Auth.User.ClientUser", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}