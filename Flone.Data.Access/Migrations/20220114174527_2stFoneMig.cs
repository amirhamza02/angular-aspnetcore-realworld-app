using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flone.Data.Repository.Migrations
{
    public partial class _2stFoneMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Markets_Category_CategoryId",
                table: "Markets");

            migrationBuilder.DropIndex(
                name: "IX_Markets_CategoryId",
                table: "Markets");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Markets");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Markets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Markets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Markets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Markets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarketId",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MarketsId",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Category_MarketsId",
                table: "Category",
                column: "MarketsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Markets_MarketsId",
                table: "Category",
                column: "MarketsId",
                principalTable: "Markets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Markets_MarketsId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_MarketsId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Markets");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Markets");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Markets");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Markets");

            migrationBuilder.DropColumn(
                name: "MarketId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "MarketsId",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Markets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Markets_CategoryId",
                table: "Markets",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Markets_Category_CategoryId",
                table: "Markets",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
