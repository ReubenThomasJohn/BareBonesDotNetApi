using Microsoft.AspNetCore.Mvc;
using StudentApi.Entities;

namespace BareBonesDotNetApi.Application.Services;

public interface IStudentsService
{
    public Task<IEnumerable<Student>?> GetAll();
    public Student? Get(int id);

    public Student Post(Student student);

    public Student Put(int id, Student updatedStudent);

    public Student? Delete(int id);
    public dynamic TestCache();
}