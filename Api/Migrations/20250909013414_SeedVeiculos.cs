using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace minimal_api.Migrations
{
    /// <inheritdoc />
    public partial class SeedVeiculos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Veiculos",
                columns: new[] { "Id", "Ano", "Marca", "Nome" },
                values: new object[,]
                {
                    { 1, 2023, "Honda", "Civic" },
                    { 2, 2024, "Toyota", "Corolla" },
                    { 3, 2022, "Volkswagen", "Golf" },
                    { 4, 2023, "Hyundai", "HB20" },
                    { 5, 2024, "Chevrolet", "Onix" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Veiculos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Veiculos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Veiculos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Veiculos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Veiculos",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
