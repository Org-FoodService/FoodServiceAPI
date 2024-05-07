using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodService.Data.Migrations
{
    /// <inheritdoc />
    public partial class shortDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Description",
                keyValue: null,
                column: "Description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Product",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Description",
                keyValue: null,
                column: "Description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Ingredient",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Ingredient",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ExpirationDate", "ShortDescription" },
                values: new object[] { "Fresh and ripe, our tomatoes are harvested at the peak of perfection, ensuring unmatched flavor and quality.", new DateTime(2024, 5, 14, 14, 31, 26, 37, DateTimeKind.Local).AddTicks(9313), "Fresh Tomato" });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ExpirationDate", "ShortDescription" },
                values: new object[] { "Our lettuces are carefully grown, offering a crisp texture and a light flavor that perfectly complements any salad.", new DateTime(2024, 5, 12, 14, 31, 26, 37, DateTimeKind.Local).AddTicks(9340), "Crisp Lettuce" });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ExpirationDate", "ShortDescription" },
                values: new object[] { "Our chicken breasts are boneless and carefully prepared to ensure tender, juicy meat, perfect for a variety of dishes.", new DateTime(2024, 5, 10, 14, 31, 26, 37, DateTimeKind.Local).AddTicks(9343), "Boneless Chicken Breast" });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ExpirationDate", "ShortDescription" },
                values: new object[] { "Our cheddar cheese is aged with care to develop its rich, creamy flavor, adding an irresistible touch to any dish.", new DateTime(2024, 5, 17, 14, 31, 26, 37, DateTimeKind.Local).AddTicks(9345), "Aged Cheddar Cheese" });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "ExpirationDate", "ShortDescription" },
                values: new object[] { "Our fresh onions are hand-selected to ensure consistent quality and flavor, adding robust, aromatic taste to any dish.", new DateTime(2024, 5, 14, 14, 31, 26, 37, DateTimeKind.Local).AddTicks(9348), "Fresh Onion" });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ExpirationDate", "ShortDescription" },
                values: new object[] { "Our fresh lemons are harvested at their peak of freshness, offering a citrusy, refreshing flavor that elevates any beverage or dish.", new DateTime(2024, 5, 17, 14, 31, 26, 37, DateTimeKind.Local).AddTicks(9350), "Fresh Lemon" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ShortDescription" },
                values: new object[] { "Our tomato soup is made with the finest fresh tomatoes, seasoned with herbs and spices for a rich, comforting flavor.", "Delicious tomato soup" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ShortDescription" },
                values: new object[] { "Our chicken salad is healthy and delicious, featuring tender chicken breast, crisp lettuce, and fresh vegetables, tossed in a tangy dressing.", "Healthy chicken salad" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ShortDescription" },
                values: new object[] { "Our lemonade is made with freshly squeezed lemons, pure cane sugar, and filtered water, creating a refreshing beverage that's perfect for any occasion.", "Refreshing lemonade" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ShortDescription" },
                values: new object[] { "Our classic cheeseburger features a juicy beef patty, melted cheddar cheese, crisp lettuce, ripe tomatoes, onions, and pickles, all served on a toasted bun.", "Classic cheeseburger" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "ShortDescription" },
                values: new object[] { "Our crispy onion rings are made with fresh onions, coated in a seasoned batter, and fried to golden perfection, creating a delicious side dish or snack.", "Crispy onion rings" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Ingredient");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Ingredient",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ExpirationDate" },
                values: new object[] { "Fresh tomato", new DateTime(2024, 5, 11, 8, 43, 25, 900, DateTimeKind.Local).AddTicks(7960) });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ExpirationDate" },
                values: new object[] { "Crispy lettuce", new DateTime(2024, 5, 9, 8, 43, 25, 900, DateTimeKind.Local).AddTicks(7982) });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ExpirationDate" },
                values: new object[] { "Boneless chicken breast", new DateTime(2024, 5, 7, 8, 43, 25, 900, DateTimeKind.Local).AddTicks(7984) });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ExpirationDate" },
                values: new object[] { "Cheddar cheese", new DateTime(2024, 5, 14, 8, 43, 25, 900, DateTimeKind.Local).AddTicks(7986) });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "ExpirationDate" },
                values: new object[] { "Fresh onion", new DateTime(2024, 5, 11, 8, 43, 25, 900, DateTimeKind.Local).AddTicks(7987) });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ExpirationDate" },
                values: new object[] { "Fresh lemon", new DateTime(2024, 5, 14, 8, 43, 25, 900, DateTimeKind.Local).AddTicks(7989) });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Delicious tomato soup");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Healthy chicken salad");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Refreshing lemonade");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Classic cheeseburger");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "Crispy onion rings");
        }
    }
}
