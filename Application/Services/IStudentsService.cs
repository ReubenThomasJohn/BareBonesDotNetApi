using Microsoft.AspNetCore.Mvc;
using StudentApi.Entities;

namespace BareBonesDotNetApi.Application.Services;

public interface IStudentsService
{
    public IActionResult GetAll();
    public IActionResult Get(int id);

    public IActionResult Post(Student student);

    public IActionResult Put(int id, Student updatedStudent);

    public IActionResult Delete(int id);
}