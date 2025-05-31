using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSecureHospitalSystem.Migrations
{
    /// <inheritdoc />
    public partial class DoctorModelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_Doctor_ID",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_ID",
                table: "Appointments",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_Doctor_ID",
                table: "Appointments",
                column: "Doctor_ID",
                principalTable: "Doctors",
                principalColumn: "Doctor_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_Doctor_ID",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_ID",
                table: "Appointments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_Doctor_ID",
                table: "Appointments",
                column: "Doctor_ID",
                principalTable: "Doctors",
                principalColumn: "Doctor_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
