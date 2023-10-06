using Microsoft.EntityFrameworkCore;
using StudentApi.Repositories;

namespace StudentApi.Data;

public static class DataExtensions
{
    public static void InitializeDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<StudentListContext>();
        dbContext.Database.Migrate();
    }

    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connString = configuration.GetConnectionString("StudentDbConnectionString");
        services.AddSqlServer<StudentListContext>(connString)
                        .AddScoped<IGamesRepository, EntityFrameworkStudentsRepository>(); // SqlServer is registered as a scoped service.
    
     return services;
    }
}