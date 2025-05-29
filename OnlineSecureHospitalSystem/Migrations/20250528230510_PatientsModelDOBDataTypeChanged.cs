using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSecureHospitalSystem.Migrations
{
    /// <inheritdoc />
    public partial class PatientsModelDOBDataTypeChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 1,
                column: "DOB",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 1,
                column: "DOB",
                value: null);
        }
    }
}
