using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Endpoints;
using StudentApi.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

app.Use(async (context, next) => // context is the http request that has all the info about the request coming into the application. // next is the next function that executes after this middleware. It could be the endpoint, or another middleware etc.

{
    try
    {
        await next(context); // if no exception, just pass onto the next function
    }
    catch (Exception e)
    {
        app.Logger.LogError(
                    e, "Could not process a request on machine {Machine}. TraceID: {TraceID}",
                    Environment.MachineName,
                    Activity.Current?.Id);

        // return Results.StatusCode(500); // not a nice experience for the client
        await Results.Problem(
            title: "We made a mistake, but we are working on it!",
            statusCode: StatusCodes.Status500InternalServerError,
            extensions: new Dictionary<string, object?>
            {
                        {"traceID", Activity.Current?.Id}
            }
        )
        .ExecuteAsync(context); // send the context here?
    }
});

app.Services.InitializeDb();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapStudentsEndpoints();
app.MapControllers();
app.Run();


