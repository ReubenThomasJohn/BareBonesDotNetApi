using StudentApi.Entities;

namespace StudentApi.Repositories;

public class InMemStudentRepository : IGamesRepository
{
    private readonly List<Student> students = new()
    {
        new Student()
        {
            Id=1,
            Name="Raj",
            Rank = 1
        },
        new Student()
        {
            Id=2,
            Name="Prakash",
            Rank = 2
        }
    };

    public IEnumerable<Student> GetAll() 
    {
        return students;
    }

    public Student? Get(int id) 
    {
        return students.Find(game => game.Id == id);
    }

    public void Create(Student student) 
    {
        student.Id = students.Max(student => student.Id) + 1;
        students.Add(student);
    }

    public void Update(Student updatedStudent)
    {
        var index = students.FindIndex(student => student.Id == updatedStudent.Id);
        students[index] = updatedStudent;
    }

    public void Delete(int id)
    {
        var index = students.FindIndex(student => student.Id == id);
        students.RemoveAt(index);
    }
}