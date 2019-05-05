using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chapter1.Migrations
{
    public partial class AddCustomOrderSellableItemRelationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomOrderSellableItemRelations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Barcode = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomOrderSellableItemRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomOrderSellableItemRelations_SellableItems_Barcode",
                        column: x => x.Barcode,
                        principalTable: "SellableItems",
                        principalColumn: "Barcode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomOrderSellableItemRelations_CustomOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "CustomOrder",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomOrderSellableItemRelations_Barcode",
                table: "CustomOrderSellableItemRelations",
                column: "Barcode");

            migrationBuilder.CreateIndex(
                name: "IX_CustomOrderSellableItemRelations_OrderId",
                table: "CustomOrderSellableItemRelations",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomOrderSellableItemRelations");
        }
    }
}
