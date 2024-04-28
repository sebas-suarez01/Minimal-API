using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Minimal_API.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class OrderMigrationCorrect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "LineItemModel");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "LineItemModel");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "LineItemModel",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                table: "LineItemModel",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ItemModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineItemModel_ItemId_OrderId",
                table: "LineItemModel",
                columns: new[] { "ItemId", "OrderId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemModel_Id",
                table: "ItemModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LineItemModel_ItemModel_ItemId",
                table: "LineItemModel",
                column: "ItemId",
                principalTable: "ItemModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineItemModel_ItemModel_ItemId",
                table: "LineItemModel");

            migrationBuilder.DropTable(
                name: "ItemModel");

            migrationBuilder.DropIndex(
                name: "IX_LineItemModel_ItemId_OrderId",
                table: "LineItemModel");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "LineItemModel");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "LineItemModel");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LineItemModel",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "LineItemModel",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
