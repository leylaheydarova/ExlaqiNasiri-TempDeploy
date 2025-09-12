using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExlaqiNasiri.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatebaseuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreadetAt",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreadetAt",
                table: "AspNetUsers");
        }
    }
}
