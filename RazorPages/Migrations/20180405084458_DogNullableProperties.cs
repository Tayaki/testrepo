using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RazorPages.Migrations
{
    public partial class DogNullableProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Dog",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Dog",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Dog",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Dog",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Dog");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Dog");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Dog",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Dog",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
