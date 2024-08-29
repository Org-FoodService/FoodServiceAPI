using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodServiceAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class OutboxMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OutboxMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessage", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpirationDate",
                value: new DateTime(2024, 9, 4, 14, 43, 43, 15, DateTimeKind.Local).AddTicks(7649));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpirationDate",
                value: new DateTime(2024, 9, 2, 14, 43, 43, 15, DateTimeKind.Local).AddTicks(7680));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpirationDate",
                value: new DateTime(2024, 8, 31, 14, 43, 43, 15, DateTimeKind.Local).AddTicks(7683));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 4,
                column: "ExpirationDate",
                value: new DateTime(2024, 9, 7, 14, 43, 43, 15, DateTimeKind.Local).AddTicks(7687));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 5,
                column: "ExpirationDate",
                value: new DateTime(2024, 9, 4, 14, 43, 43, 15, DateTimeKind.Local).AddTicks(7691));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 6,
                column: "ExpirationDate",
                value: new DateTime(2024, 9, 7, 14, 43, 43, 15, DateTimeKind.Local).AddTicks(7694));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdate",
                value: new DateTime(2024, 8, 28, 14, 43, 43, 15, DateTimeKind.Local).AddTicks(8451));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutboxMessage");

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 1,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 17, 9, 4, 55, 425, DateTimeKind.Local).AddTicks(5603));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 2,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 15, 9, 4, 55, 425, DateTimeKind.Local).AddTicks(5621));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 3,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 13, 9, 4, 55, 425, DateTimeKind.Local).AddTicks(5623));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 4,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 20, 9, 4, 55, 425, DateTimeKind.Local).AddTicks(5625));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 5,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 17, 9, 4, 55, 425, DateTimeKind.Local).AddTicks(5627));

            migrationBuilder.UpdateData(
                table: "Ingredient",
                keyColumn: "Id",
                keyValue: 6,
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 20, 9, 4, 55, 425, DateTimeKind.Local).AddTicks(5629));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdate",
                value: new DateTime(2024, 6, 10, 9, 4, 55, 425, DateTimeKind.Local).AddTicks(6057));
        }
    }
}
