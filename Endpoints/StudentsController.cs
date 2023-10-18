using BareBonesDotNetApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Entities;
using StudentApi.Repositories;

namespace BareBonesDotNetApi.Endpoints;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentsRepository repository;

    public StudentsController(IStudentsRepository repository)
    {
        this.repository = repository;
    }

    const string GetStudentEndpointName = "GetGame";

    [HttpGet("students", Name = "GetStudentById")]
    [Authorize(Roles = "Admin")]
    // [Authorize]
    public IActionResult GetAll()
    {
        return Ok(repository.GetAll());
    }

    [HttpGet("students/{id}")]
    public IActionResult Get(int id)
    {
        Student? student = repository.Get(id);
        return student is not null ? Ok(student) : NotFound();
    }

    // [HttpPost(Name = "students")]
    // public IActionResult Post(Student student)
    // {
    //     repository.Create(student);
    //     return CreatedAtRoute(GetStudentEndpointName, new { id = student.Id }, student);
    // }

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

    [HttpDelete(Name = "students/{id}")]
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
