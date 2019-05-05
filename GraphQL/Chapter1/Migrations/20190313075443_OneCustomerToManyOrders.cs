using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chapter1.Migrations
{
    public partial class OneCustomerToManyOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomCustomer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    BillingAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomCustomer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "CustomOrder",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tag = table.Column<string>(nullable: true),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    CustomerId1 = table.Column<int>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomOrder", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_CustomOrder_CustomCustomer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomCustomer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomOrder_CustomCustomer_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "CustomCustomer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomOrder_CustomerId",
                table: "CustomOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomOrder_CustomerId1",
                table: "CustomOrder",
                column: "CustomerId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomOrder");

            migrationBuilder.DropTable(
                name: "CustomCustomer");
        }
    }
}
