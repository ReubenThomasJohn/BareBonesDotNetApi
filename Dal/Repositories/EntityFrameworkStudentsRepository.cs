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

    public Student Create(Student student)
    {
        dbContext.Students.Add(student);
        dbContext.SaveChanges();

        return student;
    }

    public Student? Get(int id)
    {
        // throw new InvalidOperationException("The database connection is closed!");
        return dbContext.Students.Find(id);
    }

    public async Task<IEnumerable<Student>> GetAll()
    {
        // I made a change in Exception settings for this to work, check out exception settings
        // throw new InvalidOperationException("The database connection is closed!");
        await Task.Delay(5000);
        return await dbContext.Students.AsNoTracking().ToListAsync();  // Don't keep track of changes using AsNoTracking
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