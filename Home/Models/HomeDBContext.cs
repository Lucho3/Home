using Home.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Home.Models
{
    public class HomeDBContext: DbContext
    {
        public HomeDBContext(DbContextOptions<HomeDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserTypeModel>().HasData(
                new UserTypeModel { id = 1, type = "Administrator" },
                new UserTypeModel { id = 2, type = "Housekeeper" },
                new UserTypeModel { id = 3, type = "Client" });

            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { id = 1, type = "Cleaning and disinfection" },
                new CategoryModel { id = 2, type = "Care for pets and plants" },
                new CategoryModel { id = 3, type = "Child care" },
                new CategoryModel { id = 4, type = "Care for the elderly" });

            modelBuilder.Entity<StatusModel>().HasData(
               new StatusModel { id = 1, status = "Waiting" },
               new StatusModel { id = 2, status = "Appointed as a domestic helper" },
               new StatusModel { id = 3, status = "Fulfilled" },
               new StatusModel { id = 4, status = "Refused" });

            modelBuilder.Entity<UserModel>().HasIndex(u => u.username).IsUnique();

            modelBuilder.Entity<LocationModel>().HasIndex(u => u.name).IsUnique();

            modelBuilder.Entity<UserModel>()
                            .Property(b => b.type)
                            .HasDefaultValueSql("3");


        }



        public virtual DbSet<UserModel> Users { get; set; }

        public virtual DbSet<UserTypeModel> UserTypes { get; set; }

        public virtual DbSet<TaskModel> Tasks { get; set; }

        public virtual DbSet<StatusModel> Statuses { get; set; }

        public virtual DbSet<LocationModel> Locations { get; set; }

        public virtual DbSet<CategoryModel> Categories { get; set; }
    }
}
