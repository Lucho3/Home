﻿// <auto-generated />
using System;
using Home.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Home.Migrations
{
    [DbContext(typeof(HomeDBContext))]
    [Migration("20210425085419_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Home.Models.Entity.CategoryModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            id = 1,
                            type = "Cleaning and disinfection"
                        },
                        new
                        {
                            id = 2,
                            type = "Care for pets and plants"
                        },
                        new
                        {
                            id = 3,
                            type = "Child care"
                        },
                        new
                        {
                            id = 4,
                            type = "Care for the elderly"
                        });
                });

            modelBuilder.Entity("Home.Models.Entity.LocationModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("userid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("name")
                        .IsUnique();

                    b.HasIndex("userid");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Home.Models.Entity.StatusModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            id = 1,
                            status = "Waiting"
                        },
                        new
                        {
                            id = 2,
                            status = "Appointed as a domestic helper"
                        },
                        new
                        {
                            id = 3,
                            status = "Fulfilled"
                        },
                        new
                        {
                            id = 4,
                            status = "Refused"
                        });
                });

            modelBuilder.Entity("Home.Models.Entity.TaskModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("budget")
                        .HasColumnType("decimal");

                    b.Property<int>("categoryid")
                        .HasColumnType("int");

                    b.Property<DateTime>("deadline")
                        .HasColumnType("date");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("locationid")
                        .HasColumnType("int");

                    b.Property<int>("statusid")
                        .HasColumnType("int");

                    b.Property<int>("userid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("categoryid");

                    b.HasIndex("locationid");

                    b.HasIndex("statusid");

                    b.HasIndex("userid");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Home.Models.Entity.UserModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("typeid")
                        .HasColumnType("int");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("id");

                    b.HasIndex("typeid");

                    b.HasIndex("username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Home.Models.Entity.UserTypeModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("id");

                    b.ToTable("UserTypes");

                    b.HasData(
                        new
                        {
                            id = 1,
                            type = "Administrator"
                        },
                        new
                        {
                            id = 2,
                            type = "Housekeeper"
                        },
                        new
                        {
                            id = 3,
                            type = "Client"
                        });
                });

            modelBuilder.Entity("Home.Models.Entity.LocationModel", b =>
                {
                    b.HasOne("Home.Models.Entity.UserModel", "user")
                        .WithMany("locations")
                        .HasForeignKey("userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("Home.Models.Entity.TaskModel", b =>
                {
                    b.HasOne("Home.Models.Entity.CategoryModel", "category")
                        .WithMany("tasks")
                        .HasForeignKey("categoryid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Home.Models.Entity.LocationModel", "location")
                        .WithMany("tasks")
                        .HasForeignKey("locationid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Home.Models.Entity.StatusModel", "status")
                        .WithMany("tasks")
                        .HasForeignKey("statusid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Home.Models.Entity.UserModel", "user")
                        .WithMany("tasks")
                        .HasForeignKey("userid")
                        .HasConstraintName("FK_Tasks_Users_userid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("category");

                    b.Navigation("location");

                    b.Navigation("status");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Home.Models.Entity.UserModel", b =>
                {
                    b.HasOne("Home.Models.Entity.UserTypeModel", "type")
                        .WithMany("users")
                        .HasForeignKey("typeid");

                    b.Navigation("type");
                });

            modelBuilder.Entity("Home.Models.Entity.CategoryModel", b =>
                {
                    b.Navigation("tasks");
                });

            modelBuilder.Entity("Home.Models.Entity.LocationModel", b =>
                {
                    b.Navigation("tasks");
                });

            modelBuilder.Entity("Home.Models.Entity.StatusModel", b =>
                {
                    b.Navigation("tasks");
                });

            modelBuilder.Entity("Home.Models.Entity.UserModel", b =>
                {
                    b.Navigation("locations");

                    b.Navigation("tasks");
                });

            modelBuilder.Entity("Home.Models.Entity.UserTypeModel", b =>
                {
                    b.Navigation("users");
                });
#pragma warning restore 612, 618
        }
    }
}
