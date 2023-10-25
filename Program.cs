using BareBonesDotNetApi;
using StudentApi.Data;
using StudentApi.Endpoints;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BareBonesDotNetApi.Services;
using Serilog;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using BareBonesDotNetApi.Application.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true; // Respect client Accept headers
}).AddXmlDataContractSerializerFormatters(); // Add support for XML

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStudentsService, StudentsService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddMemoryCache();
// builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!))
    };
});

builder.Services.AddRepositories(builder.Configuration);

builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;
}).AddXmlSerializerFormatters();

builder.Logging.ClearProviders();
var env = builder.Environment;
if (env.IsDevelopment())
{

    builder.Host.UseSerilog((hostContext, services, configuration) =>
    {
        configuration
            .MinimumLevel.Debug()
            .WriteTo.File("serilog-file.txt")
            .WriteTo.Console()
            .ReadFrom.Configuration(hostContext.Configuration);
    });
}
else
{
    builder.Host.UseSerilog((hostContext, services, configuration) =>
    {
        configuration
            .WriteTo.File("serilog-file.txt")
            .WriteTo.Console();
    });
}

var app = builder.Build();

// app.UseMiddleware<GlobalExceptionMiddleware>();
// app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.ConfigureExceptionHandler());
// app.UseExceptionHandler("/Home/Error");


app.Services.InitializeDb();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// app.MapStudentsEndpoints();
app.MapControllers();
app.Run();


