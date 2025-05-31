using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSecureHospitalSystem.Migrations
{
    /// <inheritdoc />
    public partial class AppointmentModelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extra_Information",
                table: "Appointments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "Appointments",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extra_Information",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Appointments");
        }
    }
}
