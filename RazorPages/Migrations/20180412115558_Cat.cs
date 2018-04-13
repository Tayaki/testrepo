using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RazorPages.Migrations
{
    public partial class Cat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatId",
                table: "Dog",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Birth = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Sex = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dog_CatId",
                table: "Dog",
                column: "CatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dog_Cat_CatId",
                table: "Dog",
                column: "CatId",
                principalTable: "Cat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dog_Cat_CatId",
                table: "Dog");

            migrationBuilder.DropTable(
                name: "Cat");

            migrationBuilder.DropIndex(
                name: "IX_Dog_CatId",
                table: "Dog");

            migrationBuilder.DropColumn(
                name: "CatId",
                table: "Dog");
        }
    }
}
