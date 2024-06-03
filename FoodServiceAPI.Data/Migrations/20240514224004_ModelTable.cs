using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodService.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModelTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TableId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 21, 19, 40, 3, 571, DateTimeKind.Local).AddTicks(9682));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 19, 19, 40, 3, 571, DateTimeKind.Local).AddTicks(9701));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 17, 19, 40, 3, 571, DateTimeKind.Local).AddTicks(9703));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 4,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 24, 19, 40, 3, 571, DateTimeKind.Local).AddTicks(9705));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 5,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 21, 19, 40, 3, 571, DateTimeKind.Local).AddTicks(9706));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 6,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 24, 19, 40, 3, 571, DateTimeKind.Local).AddTicks(9708));

            migrationBuilder.CreateIndex(
                name: "IX_Order_TableId",
                table: "Order",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Tables_TableId",
                table: "Order",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Tables_TableId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Order_TableId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "TableId",
                table: "Order");

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 14, 14, 31, 26, 37, DateTimeKind.Local).AddTicks(9313));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 12, 14, 31, 26, 37, DateTimeKind.Local).AddTicks(9340));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 10, 14, 31, 26, 37, DateTimeKind.Local).AddTicks(9343));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 4,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 17, 14, 31, 26, 37, DateTimeKind.Local).AddTicks(9345));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 5,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 14, 14, 31, 26, 37, DateTimeKind.Local).AddTicks(9348));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 6,
                column: "ExpirationDate",
                value: new DateTime(2024, 5, 17, 14, 31, 26, 37, DateTimeKind.Local).AddTicks(9350));
        }
    }
}
