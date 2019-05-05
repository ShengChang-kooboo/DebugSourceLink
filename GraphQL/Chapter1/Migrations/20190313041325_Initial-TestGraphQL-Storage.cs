using Microsoft.EntityFrameworkCore.Migrations;

namespace Chapter1.Migrations
{
    public partial class InitialTestGraphQLStorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SellableItems",
                columns: table => new
                {
                    Barcode = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellableItems", x => x.Barcode);
                });

            migrationBuilder.InsertData(
                table: "SellableItems",
                columns: new[] { "Barcode", "SellingPrice", "Title" },
                values: new object[] { "123", 50m, "Headphone" });

            migrationBuilder.InsertData(
                table: "SellableItems",
                columns: new[] { "Barcode", "SellingPrice", "Title" },
                values: new object[] { "456", 40m, "Keyboard" });

            migrationBuilder.InsertData(
                table: "SellableItems",
                columns: new[] { "Barcode", "SellingPrice", "Title" },
                values: new object[] { "789", 100m, "Monitor" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SellableItems");
        }
    }
}
