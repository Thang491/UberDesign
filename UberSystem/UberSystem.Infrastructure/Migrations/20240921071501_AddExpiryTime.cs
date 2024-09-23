using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UberSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExpiryTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryTime",
                table: "EmailVerifications",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiryTime",
                table: "EmailVerifications");
        }
    }
}
