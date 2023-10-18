using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

namespace BareBonesDotNetApi;

public static class ErrorHandlingExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            // context is the http request that has all the info about the request coming into the application. 
            // next is the next function that executes after this middleware. 
            // It could be the endpoint, or another middleware etc.
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

            var exceptionDetails = context.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionDetails?.Error;

            logger.LogError(
                        exception, "Could not process a request on machine {Machine}. TraceID: {TraceID}",
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
        });
    }
}