using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSecureHospitalSystem.Migrations
{
    /// <inheritdoc />
    public partial class AssignmentsModelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Doctors_Curing_Doctor_ID",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "Curing_Doctor_ID",
                table: "Assignments",
                newName: "Doctor_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_Curing_Doctor_ID",
                table: "Assignments",
                newName: "IX_Assignments_Doctor_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Doctors_Doctor_ID",
                table: "Assignments",
                column: "Doctor_ID",
                principalTable: "Doctors",
                principalColumn: "Doctor_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Doctors_Doctor_ID",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "Doctor_ID",
                table: "Assignments",
                newName: "Curing_Doctor_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_Doctor_ID",
                table: "Assignments",
                newName: "IX_Assignments_Curing_Doctor_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Doctors_Curing_Doctor_ID",
                table: "Assignments",
                column: "Curing_Doctor_ID",
                principalTable: "Doctors",
                principalColumn: "Doctor_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
