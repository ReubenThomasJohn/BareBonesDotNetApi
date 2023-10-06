using System.ComponentModel.DataAnnotations;

namespace StudentApi.Entities;


public class Student
{
    [Key]
    public int Id { get; set;}
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
    public required int Rank { get; set;}
}