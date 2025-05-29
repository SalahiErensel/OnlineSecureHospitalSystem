using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineSecureHospitalSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Role_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Role_Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Role_ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    First_Name = table.Column<string>(type: "TEXT", nullable: true),
                    Last_Name = table.Column<string>(type: "TEXT", nullable: true),
                    Phone_Number = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    IsDefaultPassword = table.Column<int>(type: "INTEGER", nullable: true),
                    Role_ID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_ID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_Role_ID",
                        column: x => x.Role_ID,
                        principalTable: "Roles",
                        principalColumn: "Role_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Doctor_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    User_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Specialization = table.Column<string>(type: "TEXT", nullable: true),
                    Is_Chief = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Doctor_ID);
                    table.ForeignKey(
                        name: "FK_Doctors_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Patient_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    User_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Surname = table.Column<string>(type: "TEXT", nullable: true),
                    DOB = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: true),
                    Phone_Number = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Patient_ID);
                    table.ForeignKey(
                        name: "FK_Patients_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Appointment_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Patient_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Doctor_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Appointment_Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Appointment_Status = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Appointment_ID);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_Doctor_ID",
                        column: x => x.Doctor_ID,
                        principalTable: "Doctors",
                        principalColumn: "Doctor_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_Patient_ID",
                        column: x => x.Patient_ID,
                        principalTable: "Patients",
                        principalColumn: "Patient_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Assignment_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Patient_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Curing_Doctor_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Assigned_By = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Assignment_ID);
                    table.ForeignKey(
                        name: "FK_Assignments_Doctors_Assigned_By",
                        column: x => x.Assigned_By,
                        principalTable: "Doctors",
                        principalColumn: "Doctor_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_Doctors_Curing_Doctor_ID",
                        column: x => x.Curing_Doctor_ID,
                        principalTable: "Doctors",
                        principalColumn: "Doctor_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_Patients_Patient_ID",
                        column: x => x.Patient_ID,
                        principalTable: "Patients",
                        principalColumn: "Patient_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    Record_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Curing_Doctor_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Patient_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    Record_Data = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Signature = table.Column<string>(type: "TEXT", nullable: true),
                    HashValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Record_ID);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Doctors_Curing_Doctor_ID",
                        column: x => x.Curing_Doctor_ID,
                        principalTable: "Doctors",
                        principalColumn: "Doctor_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Patients_Patient_ID",
                        column: x => x.Patient_ID,
                        principalTable: "Patients",
                        principalColumn: "Patient_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Role_ID", "Role_Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Receptionist" },
                    { 3, "Chief Doctor" },
                    { 4, "Curing Doctor" },
                    { 5, "Consulting Doctor" },
                    { 6, "Patient" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_ID", "Address", "Email", "First_Name", "IsDefaultPassword", "Last_Name", "Password", "Phone_Number", "Role_ID", "Username" },
                values: new object[] { 1, "Kyrenia", "salahi_erensel@hotmail.com", "Admin", 1, "Admin", "123456", "5338500968", 1, "salahiadmin" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Doctor_ID",
                table: "Appointments",
                column: "Doctor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Patient_ID",
                table: "Appointments",
                column: "Patient_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_Assigned_By",
                table: "Assignments",
                column: "Assigned_By");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_Curing_Doctor_ID",
                table: "Assignments",
                column: "Curing_Doctor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_Patient_ID",
                table: "Assignments",
                column: "Patient_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_User_ID",
                table: "Doctors",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_Curing_Doctor_ID",
                table: "MedicalRecords",
                column: "Curing_Doctor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_Patient_ID",
                table: "MedicalRecords",
                column: "Patient_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_User_ID",
                table: "Patients",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role_ID",
                table: "Users",
                column: "Role_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
