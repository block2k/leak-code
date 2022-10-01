using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Students");

            migrationBuilder.AddColumn<string>(
                name: "Base64ImageString",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64ImageString",
                table: "Students");

            migrationBuilder.AddColumn<byte[]>(
                name: "Images",
                table: "Students",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
