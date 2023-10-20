using System.ComponentModel.DataAnnotations;
using StudentApi.Entities;

namespace BareBonesDotNetApi.Entities;

public class State
{
    public int Id { get; set; }
    public required string StateName { get; set; }

    // Navigation Property for students
    public ICollection<Student> Students { get; set; } = new List<Student>();
}