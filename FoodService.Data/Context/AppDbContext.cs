using FoodService.Nuget.Models;
using FoodService.Nuget.Models.Auth.Role;
using FoodService.Nuget.Models.Auth.User;
using FoodService.Nuget.Models.Enum;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Data.Context
{
    /// <summary>
    /// Database context for the application, derived from IdentityDbContext.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class.
    /// </remarks>
    public class AppDbContext : IdentityDbContext<UserBase, ApplicationRole, int>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureProductIngredients(modelBuilder);
            ConfigureInitialData(modelBuilder);
        }
        /// <summary>
        /// Configures the many-to-many relationship between products and ingredients.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        private void ConfigureProductIngredients(ModelBuilder modelBuilder)
        {
            // Configure the composite primary key for the ProductIngredient entity
            modelBuilder.Entity<ProductIngredient>()
                .HasKey(pi => new { pi.ProductId, pi.IngredientId });

            // Configure the relationship between ProductIngredient and Product entities
            modelBuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Product)               // Each ProductIngredient belongs to one Product
                .WithMany(p => p.ProductIngredients)    // Each Product can have multiple ProductIngredients
                .HasForeignKey(pi => pi.ProductId);    // Foreign key pointing to ProductId in ProductIngredient

            // Configure the relationship between ProductIngredient and Ingredient entities
            modelBuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Ingredient)            // Each ProductIngredient belongs to one Ingredient
                .WithMany(i => i.ProductIngredients)    // Each Ingredient can be used in multiple ProductIngredients
                .HasForeignKey(pi => pi.IngredientId); // Foreign key pointing to IngredientId in ProductIngredient
        }


        /// <summary>
        /// Configures initial data for Ingredients, Products, and their relationships.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        private void ConfigureInitialData(ModelBuilder modelBuilder)
        {
            // Add initial data for Ingredients
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Tomato", Description = "Fresh tomato", StockQuantity = 100, IsFresh = true, ExpirationDate = DateTime.Now.AddDays(7) },
                new Ingredient { Id = 2, Name = "Lettuce", Description = "Crispy lettuce", StockQuantity = 50, IsFresh = true, ExpirationDate = DateTime.Now.AddDays(5) },
                new Ingredient { Id = 3, Name = "Chicken Breast", Description = "Boneless chicken breast", StockQuantity = 30, IsFresh = true, ExpirationDate = DateTime.Now.AddDays(3) },
                new Ingredient { Id = 4, Name = "Cheese", Description = "Cheddar cheese", StockQuantity = 40, IsFresh = true, ExpirationDate = DateTime.Now.AddDays(10) },
                new Ingredient { Id = 5, Name = "Onion", Description = "Fresh onion", StockQuantity = 60, IsFresh = true, ExpirationDate = DateTime.Now.AddDays(7) },
                new Ingredient { Id = 6, Name = "Lemon", Description = "Fresh lemon", StockQuantity = 30, IsFresh = true, ExpirationDate = DateTime.Now.AddDays(10) }
            );

            // Add initial data for Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Tomato Soup", Description = "Delicious tomato soup", Price = 5.99m, Active = true, Type = ProductTypeEnum.Dish, Brand = "Chef's Special" },
                new Product { Id = 2, Name = "Chicken Salad", Description = "Healthy chicken salad", Price = 8.49m, Active = true, Type = ProductTypeEnum.Dish, Brand = "Healthy Kitchen" },
                new Product { Id = 3, Name = "Lemonade", Description = "Refreshing lemonade", Price = 2.99m, Active = true, Type = ProductTypeEnum.Beverage, Brand = "Fresh Drinks" },
                new Product { Id = 4, Name = "Cheeseburger", Description = "Classic cheeseburger", Price = 7.99m, Active = true, Type = ProductTypeEnum.Dish, Brand = "Burger House" },
                new Product { Id = 5, Name = "Onion Rings", Description = "Crispy onion rings", Price = 3.49m, Active = true, Type = ProductTypeEnum.Dish, Brand = "Snack Corner" }
            );

            // Add initial data for the junction entity ProductIngredient
            modelBuilder.Entity<ProductIngredient>().HasData(
                new ProductIngredient { ProductId = 1, IngredientId = 1 },  // Product: Tomato Soup       Ingredient: Tomato
                new ProductIngredient { ProductId = 1, IngredientId = 2 },  // Product: Tomato Soup       Ingredient: Lettuce
                new ProductIngredient { ProductId = 1, IngredientId = 4 },  // Product: Tomato Soup       Ingredient: Cheese
                new ProductIngredient { ProductId = 2, IngredientId = 2 },  // Product: Chicken Salad     Ingredient: Lettuce
                new ProductIngredient { ProductId = 2, IngredientId = 3 },  // Product: Chicken Salad     Ingredient: Chicken Breast
                new ProductIngredient { ProductId = 3, IngredientId = 6 },  // Product: Lemonade          Ingredient: Lemon
                new ProductIngredient { ProductId = 4, IngredientId = 1 },  // Product: Cheeseburger      Ingredient: Tomato
                new ProductIngredient { ProductId = 4, IngredientId = 2 },  // Product: Cheeseburger      Ingredient: Lettuce
                new ProductIngredient { ProductId = 4, IngredientId = 4 },  // Product: Cheeseburger      Ingredient: Cheese
                new ProductIngredient { ProductId = 5, IngredientId = 5 }   // Product: Onion Rings       Ingredient: Onion
            );
        }


        /// <summary>
        /// Represents the client users in the database.
        /// </summary>
        public DbSet<ClientUser> ClientUser { get; set; }

        /// <summary>
        /// Represents the employee users in the database.
        /// </summary>
        public DbSet<EmployeeUser> EmployeeUser { get; set; }

        /// <summary>
        /// Represents the roles in the database.
        /// </summary>
        public DbSet<ApplicationRole> Role { get; set; }

        /// <summary>
        /// Represents the products in the database.
        /// </summary>
        public DbSet<Product> Product { get; set; }

        /// <summary>
        /// Represents the ingredients in the database.
        /// </summary>
        public DbSet<Ingredient> Ingredient { get; set; }

        /// <summary>
        /// Represents the ingredients in the database.
        /// </summary>
        public DbSet<ProductIngredient> ProductIngredient { get; set; }
        /// <summary>
        /// Represents the order items in the database.
        /// </summary>
        public DbSet<OrderItem> OrderItem { get; set; }

        /// <summary>
        /// Represents the orders in the database.
        /// </summary>
        public DbSet<Order> Order { get; set; }
    }
}
