using BareBonesDotNetApi.Entities;
using Microsoft.EntityFrameworkCore;
// using StudentApi.Endpoints;
using StudentApi.Entities;
namespace StudentApi.Data;

public class StudentListContext : DbContext
{
    public StudentListContext(DbContextOptions<StudentListContext> options)
        : base(options)
    {

    }

    public DbSet<Student> Students => Set<Student>(); // This creates an empty initial instance.
    public DbSet<State> States => Set<State>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserStatus> UserStatus => Set<UserStatus>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<State>()
        .HasMany(e => e.Students)
        .WithOne(e => e.State)
        .HasForeignKey(e => e.StateId)
        .IsRequired();

        modelBuilder.Entity<State>().HasData(
        new State
        {
            Id = 1,
            StateName = "Kerala"
        },
        new State
        {
            Id = 2,
            StateName = "Tamil Nadu"
        });

        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = 1,
                Name = "Raj",
                Rank = 1,
                StateId = 1 // Associate this student with State1
            },
            new Student
            {
                Id = 2,
                Name = "Prakash",
                Rank = 2,
                StateId = 2 // Associate this student with State2
            });

        // modelBuilder.Entity<UserStatus>()
        // .HasMany(e => e.Users)
        // .WithOne(e => e.UserStatus)
        // .HasForeignKey(e => e.StatusId)
        // .IsRequired();

        modelBuilder.Entity<User>()
        .HasOne(u => u.Status)
        .WithMany()
        .HasForeignKey(u => u.UserStatusId);

        modelBuilder.Entity<UserStatus>().HasData(
        new UserStatus
        {
            Id = 1,
            Name = "Verified"
        },
        new UserStatus
        {
            Id = 2,
            Name = "Not Verified"
        },
        new UserStatus
        {
            Id = 3,
            Name = "Active"
        },
        new UserStatus
        {
            Id = 4,
            Name = "Blocked"
        },
        new UserStatus
        {
            Id = 5,
            Name = "Deleted"
        }
        );

        modelBuilder.Entity<User>().HasData(
        new User
        {
            Username = "Reuben",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Pass@word123"),
            UserStatusId = 1
        });
    }
}

