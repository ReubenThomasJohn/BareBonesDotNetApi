﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentApi.Data;

#nullable disable

namespace StudentApi.Data.Migrations
{
    [DbContext(typeof(StudentListContext))]
    [Migration("20231017040312_AddingAuthenticationSupport1")]
    partial class AddingAuthenticationSupport1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BareBonesDotNetApi.Entities.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("States");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            StateName = "Kerala"
                        },
                        new
                        {
                            Id = 2,
                            StateName = "Tamil Nadu"
                        });
                });

            modelBuilder.Entity("BareBonesDotNetApi.Entities.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserStatusId")
                        .HasColumnType("int");

                    b.HasKey("Username");

                    b.HasIndex("UserStatusId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Username = "Reuben",
                            PasswordHash = "$2a$11$pqKoPi/x.A6xVqstF6dDGOrHJJWkSbdpLx024v5qpLDQiSLZl96KK",
                            UserStatusId = 1
                        });
                });

            modelBuilder.Entity("BareBonesDotNetApi.Entities.UserStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserStatus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Verified"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Not Verified"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Active"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Blocked"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Deleted"
                        });
                });

            modelBuilder.Entity("StudentApi.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Raj",
                            Rank = 1,
                            StateId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Prakash",
                            Rank = 2,
                            StateId = 2
                        });
                });

            modelBuilder.Entity("BareBonesDotNetApi.Entities.User", b =>
                {
                    b.HasOne("BareBonesDotNetApi.Entities.UserStatus", "Status")
                        .WithMany()
                        .HasForeignKey("UserStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("StudentApi.Entities.Student", b =>
                {
                    b.HasOne("BareBonesDotNetApi.Entities.State", "State")
                        .WithMany("Students")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("BareBonesDotNetApi.Entities.State", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
