using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Entities;

namespace StudentApi.Repositories;

public class EntityFrameworkStudentsRepository : IStudentsRepository
{
    private readonly StudentListContext dbContext;

    public EntityFrameworkStudentsRepository(StudentListContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Create(Student student)
    {
        dbContext.Students.Add(student);
        dbContext.SaveChanges();
    }

    public Student? Get(int id)
    {
        return dbContext.Students.Find(id);
    }

    public IEnumerable<Student> GetAll()
    {
        return dbContext.Students.AsNoTracking().ToList();  // Don't keep track of changes using AsNoTracking
    }

    public void Update(Student updatedStudent)
    {
        dbContext.Update(updatedStudent);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        dbContext.Students.Where(student => student.Id == id)
                            .ExecuteDelete();
    }
}