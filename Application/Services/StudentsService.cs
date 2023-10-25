using System.Dynamic;
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
    public Student? Delete(int id)
    {
        Student? student = repository.Get(id);

        if (student is not null)
        {
            repository.Delete(id);
        }
        return student;
    }

    public Student? Get(int id)
    {
        Student? student = repository.Get(id);
        return student;
    }

    public async Task<IEnumerable<Student>?> GetAll()
    {
        IEnumerable<Student> allStudents = new List<Student>();
        // allStudents = memoryCache.GetOrCreate<IEnumerable<Student?>>("students", entry => allStudents);
        allStudents = memoryCache.Get<IEnumerable<Student>>("students");

        if (allStudents is not null)
        {
            var firstRecordInCashe = allStudents.ToList()[0];
            bool isTestInputInCache = firstRecordInCashe.Name == "Anish";
            if (isTestInputInCache)
            {
                allStudents = await repository.GetAll(); // introduces a 5s delay
                memoryCache.Set("students", allStudents, TimeSpan.FromMinutes(1));
            }
        }

        else if (allStudents is null)
        {
            allStudents = await repository.GetAll(); // introduces a 5s delay
            memoryCache.Set("students", allStudents, TimeSpan.FromMinutes(1));
        }
        // allStudents = await repository.GetAll(); // introduces a 5s delay
        // memoryCache.Set("students", allStudents, TimeSpan.FromMinutes(1));
        return allStudents;
    }

    public Student Post(Student student)
    {
        var createdStudent = repository.Create(student);
        return createdStudent;
    }

    public Student Put(int id, Student updatedStudent)
    {
        Student? existingStudent = repository.Get(id);

        if (existingStudent is null)
        {
            return null;
        }

        existingStudent.Name = updatedStudent.Name;
        existingStudent.Rank = updatedStudent.Rank;
        existingStudent.StateId = updatedStudent.StateId;

        repository.Update(existingStudent);
        return existingStudent;
    }
    public dynamic TestCache()
    {
        dynamic runTimeObject = new ExpandoObject();

        bool isRecordSame = CheckingCacheFromPrivateFn("students");
        runTimeObject.isRecordSame = isRecordSame;
        runTimeObject.StudentFromInsert = "World";
        runTimeObject.StudentFromAccess = "Hello";
        return runTimeObject;
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
