using Newtonsoft.Json;
using StudentApi.Entities;
using StudentApi.Repositories;

namespace StudentApi.Endpoints;

public static class StudentsEndpoints
{
    const string GetStudentEndpointName = "GetGame";
    public static RouteGroupBuilder MapStudentsEndpoints(this IEndpointRouteBuilder routes)
    {

        var group = routes.MapGroup("/students");
        //.WithParameterValidation();


        group.MapGet("/", (IGamesRepository repository) => repository.GetAll());

        group.MapGet("/{id}", (IGamesRepository repository, int id) =>
        {
            Student? student = repository.Get(id);
            return student is not null ? Results.Ok(student) : Results.NotFound();
        }).WithName(GetStudentEndpointName);

        group.MapPost("/", (IGamesRepository repository, Student student) =>
        {
            repository.Create(student);
            return Results.CreatedAtRoute(GetStudentEndpointName, new { id = student.Id }, student);
        });

        group.MapPut("/{id}", (IGamesRepository repository, int id, Student updatedStudent) =>
        {
            Student? existingStudent = repository.Get(id);

            if (existingStudent is null)
            {
                return Results.NotFound();
            }

            existingStudent.Name = updatedStudent.Name;
            existingStudent.Rank = updatedStudent.Rank;

            repository.Update(existingStudent);

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (IGamesRepository repository, int id) =>
        {
            Student? student = repository.Get(id);

            if (student is not null)
            {
                repository.Delete(id);
            }
            return Results.NoContent();
        });
        return group;
    }
}