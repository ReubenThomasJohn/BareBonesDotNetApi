using System.Diagnostics;

namespace BareBonesDotNetApi;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<GlobalExceptionMiddleware> logger;

    public GlobalExceptionMiddleware(
        ILogger<GlobalExceptionMiddleware> logger,
        RequestDelegate next)
    {
        this.logger = logger;
        this.next = next;
    }
    

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext); // if no exception, just pass onto the next function
        }
        catch (Exception e)
        {
            logger.LogError(
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
            .ExecuteAsync(httpContext); // send the context here?
        }
    }
}