using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSecureHospitalSystem.Migrations
{
    /// <inheritdoc />
    public partial class UsersModelDOBChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 1,
                column: "DOB",
                value: null);
        }
    }
}
