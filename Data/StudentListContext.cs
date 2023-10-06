using Microsoft.EntityFrameworkCore;
using StudentApi.Endpoints;
using StudentApi.Entities;
namespace StudentApi.Data;

public class StudentListContext : DbContext
{
    public StudentListContext(DbContextOptions<StudentListContext> options)
        : base(options) 
        {

        }

        public DbSet<Student> Students => Set<Student>(); // This creates an empty initial instance.

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasData(
        new Student 
        {
            Id = 1, 
            Name = "Raj", 
            Rank = 1
        },
        new Student 
        {
            Id=2,
            Name="Prakash",
            Rank = 2
        });
    }
}