using System.Security.Claims;

namespace BareBonesDotNetApi.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserService(IHttpContextAccessor _httpContextAccessor)
    {
        httpContextAccessor = _httpContextAccessor;
    }
    public string GetMyName()
    {
        var result = string.Empty;
        if (httpContextAccessor.HttpContext != null)
        {
            result = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }
        return result;
    }
}