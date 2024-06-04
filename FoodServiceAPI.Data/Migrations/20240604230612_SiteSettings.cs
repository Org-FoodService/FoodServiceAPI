using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodServiceAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class SiteSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PrimaryColor = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecondaryColor = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BackgroundColor = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ServiceName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Icon = table.Column<byte[]>(type: "longblob", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 11, 20, 6, 11, 410, DateTimeKind.Local).AddTicks(8855));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 9, 20, 6, 11, 410, DateTimeKind.Local).AddTicks(8874));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 7, 20, 6, 11, 410, DateTimeKind.Local).AddTicks(8875));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 4,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 14, 20, 6, 11, 410, DateTimeKind.Local).AddTicks(8877));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 5,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 11, 20, 6, 11, 410, DateTimeKind.Local).AddTicks(8879));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 6,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 14, 20, 6, 11, 410, DateTimeKind.Local).AddTicks(8881));

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "BackgroundColor", "Icon", "LastUpdate", "PrimaryColor", "SecondaryColor", "ServiceName" },
                values: new object[] { 1, "#ecf0f1", null, new DateTime(2024, 6, 4, 20, 6, 11, 410, DateTimeKind.Local).AddTicks(9354), "#3498db", "#2ecc71", "FoodService" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteSettings");

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 10, 17, 5, 43, 349, DateTimeKind.Local).AddTicks(557));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 8, 17, 5, 43, 349, DateTimeKind.Local).AddTicks(580));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 6, 17, 5, 43, 349, DateTimeKind.Local).AddTicks(582));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 4,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 13, 17, 5, 43, 349, DateTimeKind.Local).AddTicks(584));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 5,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 10, 17, 5, 43, 349, DateTimeKind.Local).AddTicks(586));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 6,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 13, 17, 5, 43, 349, DateTimeKind.Local).AddTicks(588));
        }
    }
}
