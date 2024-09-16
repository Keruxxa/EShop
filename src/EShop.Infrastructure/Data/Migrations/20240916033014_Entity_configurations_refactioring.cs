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
                name: "FK_BasketsItems_Baskets_BasketId",
                table: "BasketsItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketsItems_Products_ProductId",
                table: "BasketsItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteProducts_Favorites_FavoriteId",
                table: "FavoriteProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketsItems",
                table: "BasketsItems");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Favorites",
                newName: "Id");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Baskets");

            migrationBuilder.RenameTable(
                name: "BasketsItems",
                newName: "BasketItems");

            migrationBuilder.RenameIndex(
                name: "IX_BasketsItems_ProductId",
                table: "BasketItems",
                newName: "IX_BasketItems_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "Id",
                table: "Favorites",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "Id",
                table: "Baskets",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketItems",
                table: "BasketItems",
                columns: new[] { "BasketId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Baskets_BasketId",
                table: "BasketItems",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Products_ProductId",
                table: "BasketItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProducts_Favorites_FavoriteId",
                table: "FavoriteProducts",
                column: "FavoriteId",
                principalTable: "Favorites",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Users_UserId",
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
                name: "FK_FavoriteProducts_Favorites_FavoriteId",
                table: "FavoriteProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Users_UserId",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "Id",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "Id",
                table: "Baskets");

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
                name: "Id",
                table: "Favorites",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Baskets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Baskets",
                table: "Baskets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketsItems",
                table: "BasketsItems",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteProducts_Favorites_FavoriteId",
                table: "FavoriteProducts",
                column: "FavoriteId",
                principalTable: "Favorites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
