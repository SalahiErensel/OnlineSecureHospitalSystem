using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSecureHospitalSystem.Migrations
{
    /// <inheritdoc />
    public partial class MedicalRecordsModelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Appointment_ID",
                table: "MedicalRecords",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Is_Sensitive",
                table: "MedicalRecords",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_Appointment_ID",
                table: "MedicalRecords",
                column: "Appointment_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Appointments_Appointment_ID",
                table: "MedicalRecords",
                column: "Appointment_ID",
                principalTable: "Appointments",
                principalColumn: "Appointment_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Appointments_Appointment_ID",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_Appointment_ID",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "Appointment_ID",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "Is_Sensitive",
                table: "MedicalRecords");
        }
    }
}
