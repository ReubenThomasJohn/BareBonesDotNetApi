using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StudentApi.Entities;
using StudentApi.Repositories;

namespace BareBonesDotNetApi.Application.Services;

public class StudentsService : IStudentsService
{
    private readonly IMemoryCache memoryCache;
    private readonly IStudentsRepository repository;

    public StudentsService(IStudentsRepository repository, IMemoryCache memoryCache)
    {
        this.repository = repository;
        this.memoryCache = memoryCache;
    }
    public IActionResult Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IActionResult Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult?> GetAll()
    {
        throw new NotImplementedException();
    }

    public IActionResult Post(Student student)
    {
        throw new NotImplementedException();
    }

    public IActionResult Put(int id, Student updatedStudent)
    {
        throw new NotImplementedException();
    }

    public bool CheckingCacheFromPrivateFn(string cache)
    {
        var studentPassedIntoCache = ChangeCache(cache);
        var studentAccessedFromCache = memoryCache.Get<IEnumerable<Student>>(cache).ToList()[0];
        // DisposeCache();

        return studentPassedIntoCache == studentAccessedFromCache;
    }

    private void DisposeCache()
    {
        memoryCache.Dispose();
    }

    private Student ChangeCache(string cacheKey)
    {
        var newStudents = new List<Student>();
        var student = new Student()
        {
            Name = "Anish",
            Rank = 2,
            StateId = 1
        };
        newStudents.Add(student);
        memoryCache.Set(cacheKey, newStudents, TimeSpan.FromMinutes(1));
        return student;
    }
}
