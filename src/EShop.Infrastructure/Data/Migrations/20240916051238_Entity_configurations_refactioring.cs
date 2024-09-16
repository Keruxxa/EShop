using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Entity_configurations_refactioring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Users_UserId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketsItems_Baskets_BasketId",
                table: "BasketsItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketsItems_Products_ProductId",
                table: "BasketsItems");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_UserId",
                table: "Baskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketsItems",
                table: "BasketsItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BasketsItems");

            migrationBuilder.RenameTable(
                name: "BasketsItems",
                newName: "BasketItems");

            migrationBuilder.RenameIndex(
                name: "IX_BasketsItems_ProductId",
                table: "BasketItems",
                newName: "IX_BasketItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketsItems_BasketId",
                table: "BasketItems",
                newName: "IX_BasketItems_BasketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketItems",
                table: "BasketItems",
                columns: new[] { "BasketId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                table: "BasketItems",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Products_ProductId",
                table: "BasketItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Users_Id",
                table: "Baskets",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Users_Id",
                table: "Favorites",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Products_ProductId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Users_Id",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Users_Id",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketItems",
                table: "BasketItems");

            migrationBuilder.RenameTable(
                name: "BasketItems",
                newName: "BasketsItems");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_ProductId",
                table: "BasketsItems",
                newName: "IX_BasketsItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketItems_BasketId",
                table: "BasketsItems",
                newName: "IX_BasketsItems_BasketId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Favorites",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Baskets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "BasketsItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketsItems",
                table: "BasketsItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_UserId",
                table: "Baskets",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Users_UserId",
                table: "Baskets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketsItems_Baskets_BasketId",
                table: "BasketsItems",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketsItems_Products_ProductId",
                table: "BasketsItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
