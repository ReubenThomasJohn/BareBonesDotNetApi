namespace BareBonesDotNetApi.Domain.Dtos;

public class StudentDto
{
    public required string Name { get; set; }
    public required int Rank { get; set; }

    // Required Foreign key property
    public int StateId { get; set; }
}