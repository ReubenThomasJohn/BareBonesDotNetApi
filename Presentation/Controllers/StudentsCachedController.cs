using BareBonesDotNetApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Entities;
using StudentApi.Repositories;
using BareBonesDotNetApi.Application.Services;
using Microsoft.Extensions.Caching.Memory;

namespace BareBonesDotNetApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsCachedController : ControllerBase, IStudentsService
{
    private readonly IStudentsRepository repository;
    private readonly IMemoryCache memoryCache;

    public StudentsCachedController(IStudentsRepository repository, IMemoryCache memoryCache)
    {
        this.memoryCache = memoryCache;
        this.repository = repository;
    }

    const string GetStudentEndpointName = "GetStudentById";

    [HttpGet("students", Name = "GetAllStudents")]
    // [Route("/students")]
    // [Authorize(Roles = "Admin")]
    // [Authorize]
    public async Task<IActionResult?> GetAll()
    {
        IEnumerable<Student> allStudents;
        allStudents = memoryCache.Get<IEnumerable<Student>>("students");

        if (allStudents is null)
        {
            allStudents = await repository.GetAll(); // introduces a 5s delay
            memoryCache.Set("students", allStudents, TimeSpan.FromMinutes(1));
        }
        return Ok(allStudents);
    }

    [HttpGet("students/{id}", Name = "GetStudentById")]
    public IActionResult Get(int id)
    {
        Student? student = repository.Get(id);
        return student is not null ? Ok(student) : NotFound();
    }

    [HttpPost("students")]
    public IActionResult Post(Student student)
    {
        repository.Create(student);
        return CreatedAtRoute(GetStudentEndpointName, new { id = student.Id }, student);
    }

    [HttpPut("students/{id}")]
    public IActionResult Put(int id, Student updatedStudent)
    {
        Student? existingStudent = repository.Get(id);

        if (existingStudent is null)
        {
            return NotFound();
        }

        existingStudent.Name = updatedStudent.Name;
        existingStudent.Rank = updatedStudent.Rank;
        existingStudent.StateId = updatedStudent.StateId;

        repository.Update(existingStudent);

        return NoContent();
    }

    [HttpDelete("students/{id}")]
    public IActionResult Delete(int id)
    {
        Student? student = repository.Get(id);

        if (student is not null)
        {
            repository.Delete(id);
        }
        return NoContent();
    }
}