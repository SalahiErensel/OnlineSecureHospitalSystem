using Microsoft.EntityFrameworkCore;
using OnlineSecureHospitalSystem.Data.Models;

namespace OnlineSecureHospitalSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<Doctors> Doctors { get; set; }

        public DbSet<Patients> Patients { get; set; }

        public DbSet<Appointments> Appointments { get; set; }

        public DbSet<Assignments> Assignments { get; set; }

        public DbSet<MedicalRecords> MedicalRecords { get; set; }

        public DbSet<Logs> Logs { get; set; }

        //Creating default roles and admin user

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().HasData(
                new Roles { Role_ID = 1, Role_Name = "Admin" },
                new Roles { Role_ID = 2, Role_Name = "Receptionist" },
                new Roles { Role_ID = 3, Role_Name = "Chief Doctor" },
                new Roles { Role_ID = 4, Role_Name = "Curing Doctor" },
                new Roles { Role_ID = 5, Role_Name = "Consulting Doctor" },
                new Roles { Role_ID = 6, Role_Name = "Patient" }
            );

            modelBuilder.Entity<Users>().HasData(new Users
            {
                User_ID = 1,
                Username = "salahiadmin",
                Password = "123456",
                First_Name = "Admin",
                Last_Name = "Admin",
                Phone_Number = "5338500968",
                Address = "Kyrenia",
                Email = "salahi_erensel@hotmail.com",
                IsDefaultPassword = 1,
                Role_ID = 1
            });
        }
    }
}