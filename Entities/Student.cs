using System.ComponentModel.DataAnnotations;
using BareBonesDotNetApi.Entities;

namespace StudentApi.Entities;
public class Student
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int Rank { get; set; }

    // Required Foreign key property
    public int StateId { get; set; }

    // Navigation property for the state
    public State State { get; set; } = null!;
}