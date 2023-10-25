using System.ComponentModel.DataAnnotations;
using BareBonesDotNetApi.Entities;

namespace StudentApi.Entities;
public class Student
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Rank { get; set; }
    [Required]
    // Required Foreign key property
    public int StateId { get; set; }

    // Navigation property for the state
    public State State { get; set; } = null!;
}