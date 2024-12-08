using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EShop.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Order_Delivery_Status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderDeliveryStatusId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "OrderDeliveryStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDeliveryStatuses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "OrderDeliveryStatuses",
                columns: new[] { "Id", "Status" },
                values: new object[,]
                {
                    { 1, "Оплачен" },
                    { 2, "В пути" },
                    { 3, "Получен" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderDeliveryStatusId",
                table: "Orders",
                column: "OrderDeliveryStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveryStatuses_Status",
                table: "OrderDeliveryStatuses",
                column: "Status",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderDeliveryStatuses_OrderDeliveryStatusId",
                table: "Orders",
                column: "OrderDeliveryStatusId",
                principalTable: "OrderDeliveryStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderDeliveryStatuses_OrderDeliveryStatusId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderDeliveryStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderDeliveryStatusId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderDeliveryStatusId",
                table: "Orders");
        }
    }
}
