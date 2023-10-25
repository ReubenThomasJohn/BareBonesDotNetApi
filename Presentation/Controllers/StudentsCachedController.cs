using Microsoft.AspNetCore.Mvc;
using StudentApi.Entities;
using BareBonesDotNetApi.Application.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BareBonesDotNetApi.Domain.Dtos;

namespace BareBonesDotNetApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsCachedController : ControllerBase
{
    private readonly IStudentsService studentsService;

    public StudentsCachedController(IStudentsService studentsService)
    {
        this.studentsService = studentsService;
    }

    const string GetStudentEndpointName = "GetStudentById";

    [HttpGet("students", Name = "GetAllStudents")]
    // [Route("/students")]
    // [Authorize(Roles = "Admin")]
    // [Authorize]
    public async Task<IActionResult?> GetAll()
    {
        var allStudents = await studentsService.GetAll();
        return Ok(allStudents);
    }

    [HttpGet("students/{id}", Name = "GetStudentById")]
    public IActionResult Get(int id)
    {
        var student = studentsService.Get(id);
        return student is not null ? Ok(student) : NotFound();
    }

    [HttpPost("students")]
    public IActionResult Post([FromBody] StudentDto student)
    {
        // if (ModelState.IsValid)
        // {
        //     var createdStudent = studentsService.Post(student);
        //     return CreatedAtRoute(GetStudentEndpointName, new { id = createdStudent.Id }, createdStudent);
        // }

        // Console.WriteLine("HEHEHE");
        // return BadRequest();

        var postStudent = new Student()
        {
            Name = student.Name,
            Rank = student.Rank,
            StateId = student.StateId,
        };

        var createdStudent = studentsService.Post(postStudent);
        return CreatedAtRoute(GetStudentEndpointName, new { id = createdStudent.Id }, createdStudent);
    }

    [HttpPut("students/{id}")]
    public IActionResult Put(int id, StudentDto updatedStudent)
    {
        var studentToBeUpdated = new Student()
        {
            Name = updatedStudent.Name,
            Rank = updatedStudent.Rank,
            StateId = updatedStudent.StateId,
        };
        var studentUpdatedinDb = studentsService.Put(id, studentToBeUpdated);

        if (studentUpdatedinDb is null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("students/{id}")]
    public IActionResult Delete(int id)
    {
        var deletedStudent = studentsService.Delete(id);
        return NoContent();
    }

    [HttpGet("students/test-cache")]
    public IActionResult TestCache()
    {
        dynamic runTimeObject = studentsService.TestCache();

        return Ok(runTimeObject);
    }
}