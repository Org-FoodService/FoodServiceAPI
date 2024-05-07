using FoodService.Models;
using FoodService.Models.Auth.Role;
using FoodService.Models.Auth.User;
using FoodService.Models.Enum;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodServiceAPI.Data.Context
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
                new Ingredient { Id = 1, Name = "Tomato", Description = "Fresh and ripe, our tomatoes are harvested at the peak of perfection, ensuring unmatched flavor and quality.", StockQuantity = 100, ShortDescription = "Fresh Tomato", IsFresh = true, ExpirationDate = DateTime.Now.AddDays(7) },
                new Ingredient { Id = 2, Name = "Lettuce", Description = "Our lettuces are carefully grown, offering a crisp texture and a light flavor that perfectly complements any salad.", StockQuantity = 50, ShortDescription = "Crisp Lettuce", IsFresh = true, ExpirationDate = DateTime.Now.AddDays(5) },
                new Ingredient { Id = 3, Name = "Chicken Breast", Description = "Our chicken breasts are boneless and carefully prepared to ensure tender, juicy meat, perfect for a variety of dishes.", StockQuantity = 30, ShortDescription = "Boneless Chicken Breast", IsFresh = true, ExpirationDate = DateTime.Now.AddDays(3) },
                new Ingredient { Id = 4, Name = "Cheese", Description = "Our cheddar cheese is aged with care to develop its rich, creamy flavor, adding an irresistible touch to any dish.", StockQuantity = 40, ShortDescription = "Aged Cheddar Cheese", IsFresh = true, ExpirationDate = DateTime.Now.AddDays(10) },
                new Ingredient { Id = 5, Name = "Onion", Description = "Our fresh onions are hand-selected to ensure consistent quality and flavor, adding robust, aromatic taste to any dish.", StockQuantity = 60, ShortDescription = "Fresh Onion", IsFresh = true, ExpirationDate = DateTime.Now.AddDays(7) },
                new Ingredient { Id = 6, Name = "Lemon", Description = "Our fresh lemons are harvested at their peak of freshness, offering a citrusy, refreshing flavor that elevates any beverage or dish.", StockQuantity = 30, ShortDescription = "Fresh Lemon", IsFresh = true, ExpirationDate = DateTime.Now.AddDays(10) }
            );

            // Add initial data for Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Tomato Soup", Description = "Our tomato soup is made with the finest fresh tomatoes, seasoned with herbs and spices for a rich, comforting flavor.", ShortDescription = "Delicious tomato soup", Price = 5.99m, Active = true, Type = ProductTypeEnum.Dish, Brand = "Chef's Special" },
                new Product { Id = 2, Name = "Chicken Salad", Description = "Our chicken salad is healthy and delicious, featuring tender chicken breast, crisp lettuce, and fresh vegetables, tossed in a tangy dressing.", ShortDescription = "Healthy chicken salad", Price = 8.49m, Active = true, Type = ProductTypeEnum.Dish, Brand = "Healthy Kitchen" },
                new Product { Id = 3, Name = "Lemonade", Description = "Our lemonade is made with freshly squeezed lemons, pure cane sugar, and filtered water, creating a refreshing beverage that's perfect for any occasion.", ShortDescription = "Refreshing lemonade", Price = 2.99m, Active = true, Type = ProductTypeEnum.Beverage, Brand = "Fresh Drinks" },
                new Product { Id = 4, Name = "Cheeseburger", Description = "Our classic cheeseburger features a juicy beef patty, melted cheddar cheese, crisp lettuce, ripe tomatoes, onions, and pickles, all served on a toasted bun.", ShortDescription = "Classic cheeseburger", Price = 7.99m, Active = true, Type = ProductTypeEnum.Dish, Brand = "Burger House" },
                new Product { Id = 5, Name = "Onion Rings", Description = "Our crispy onion rings are made with fresh onions, coated in a seasoned batter, and fried to golden perfection, creating a delicious side dish or snack.", ShortDescription = "Crispy onion rings", Price = 3.49m, Active = true, Type = ProductTypeEnum.Dish, Brand = "Snack Corner" }
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
