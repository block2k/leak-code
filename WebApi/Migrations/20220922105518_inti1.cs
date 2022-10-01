using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class inti1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Students");

            migrationBuilder.AddColumn<byte[]>(
                name: "Images",
                table: "Students",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Students");

            migrationBuilder.AddColumn<byte>(
                name: "Image",
                table: "Students",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
