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
               new StatusModel { id = 4, status = "Refused" },
                new StatusModel { id = 5, status = "For review" });
              

            modelBuilder.Entity<UserModel>().HasIndex(u => u.username).IsUnique();

            modelBuilder.Entity<LocationModel>().HasIndex(u => u.name).IsUnique();

            modelBuilder.Entity<TaskModel>()
            .HasOne(p => p.user)
            .WithMany(q => q.tasks)
            .HasConstraintName("FK_Tasks_Users_userid");

            //Cascade

            modelBuilder.Entity<UserModel>().HasMany(r => r.tasks).WithOne(r => r.user).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskModel>().HasOne(r => r.user).WithMany(r => r.tasks).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TaskModel>().HasOne(r => r.category).WithMany(r => r.tasks).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TaskModel>().HasOne(r => r.status).WithMany(r => r.tasks).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserModel>().HasMany(t => t.locations).WithOne(r => r.user).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LocationModel>().HasMany(t => t.tasks).WithOne(u => u.location).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoryModel>().HasMany(t => t.tasks).WithOne(u => u.category).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StatusModel>().HasMany(t => t.tasks).WithOne(u => u.status).OnDelete(DeleteBehavior.Cascade);           
        }



        public virtual DbSet<UserModel> Users { get; set; }

        public virtual DbSet<UserTypeModel> UserTypes { get; set; }

        public virtual DbSet<TaskModel> Tasks { get; set; }

        public virtual DbSet<StatusModel> Statuses { get; set; }

        public virtual DbSet<LocationModel> Locations { get; set; }

        public virtual DbSet<CategoryModel> Categories { get; set; }
    }
}
