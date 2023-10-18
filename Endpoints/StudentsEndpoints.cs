using System.Diagnostics;
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


        group.MapGet("/", (IStudentsRepository repository, ILogger<Program> Logger) =>
        {
            try
            {
                return Results.Ok(repository.GetAll());
            }
            catch (Exception ex)
            {
                Logger.LogError(
                    ex, "Couldnot process a request on machine {Machine}. TraceID: {TraceID}",
                    Environment.MachineName,
                    Activity.Current?.Id);

                // return Results.StatusCode(500); // not a nice experience for the client
                return Results.Problem(
                    title: "We made a mistake, but we are working on it!",
                    statusCode: StatusCodes.Status500InternalServerError,
                    extensions: new Dictionary<string, object?>
                    {
                        {"traceID", Activity.Current?.Id}
                    }
                );
            }
        });


        group.MapGet("/{id}", (IStudentsRepository repository, int id) =>
        {
            Student? student = repository.Get(id);
            return student is not null ? Results.Ok(student) : Results.NotFound();
        }).WithName(GetStudentEndpointName);

        group.MapPost("/", (IStudentsRepository repository, Student student) =>
        {
            repository.Create(student);
            return Results.CreatedAtRoute(GetStudentEndpointName, new { id = student.Id }, student);
        });

        group.MapPut("/{id}", (IStudentsRepository repository, int id, Student updatedStudent) =>
        {
            Student? existingStudent = repository.Get(id);

            if (existingStudent is null)
            {
                return Results.NotFound();
            }

            existingStudent.Name = updatedStudent.Name;
            existingStudent.Rank = updatedStudent.Rank;
            existingStudent.StateId = updatedStudent.StateId;

            repository.Update(existingStudent);

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (IStudentsRepository repository, int id) =>
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