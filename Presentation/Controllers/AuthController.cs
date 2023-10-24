using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BareBonesDotNetApi.Entities;
using BareBonesDotNetApi.Repositories;
using BareBonesDotNetApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BareBonesDotNetApi.Endpoints;

[Route("api/[controller]")]
// [Consumes("application/json", "application/xml")] // Set default input content types
// [Produces("application/json", "application/xml")]
[ApiController]
public class AuthController : ControllerBase
{
    // public static User user = new User();
    private readonly IConfiguration _configuration;
    private readonly IUsersRepository _repository;
    private readonly IUserService userService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IConfiguration configuration, IUsersRepository repository, IUserService userService, ILogger<AuthController> logger)
    {
        _configuration = configuration;
        _repository = repository;
        this.userService = userService;
        _logger = logger;
    }

    [HttpGet("get-me"), Authorize]
    public ActionResult<string> GetMe()
    {
        var userName = userService.GetMyName();
        return Ok(userName);

        // var userName = User.Identity?.Name;
        // var userName2 = User.FindFirstValue(ClaimTypes.Name);
        // var role = User.FindFirstValue(ClaimTypes.Role);
        // return Ok(new { userName, userName2, role });
    }

    [HttpPost("register")]
    // [Consumes("application/json", "application/xml")]
    // [Produces("application/json", "application/xml")]
    public async Task<IActionResult> Register([FromBody] UserDto request)
    {
        _logger.LogInformation("CUSTOM LOG:: ENTERING REGISTER ENDPOINT!!");
        if (request == null)
        {
            return BadRequest("Invalid data.");
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // Not checking if username exists in DB, so Global exception can be thrown...

        var user = new User()
        {
            Username = request.Username,
            PasswordHash = passwordHash,
            UserStatusId = 2
        };

        var addedUser = await _repository.Create(user); // exception will be thrown here

        if (addedUser != null)
        {
            // Handle content negotiation to return JSON or XML
            var contentType = Request.ContentType;
            if (contentType == "application/xml")
            {
                // Return XML
                return Ok(addedUser);
            }
            else
            {
                // Return JSON (default)
                return Ok(addedUser);
            }
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDto request)
    {
        var user = await _repository.Get(request.Username);
        if (user == null)
        {
            return BadRequest("User not found.");
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return BadRequest("Wrong password. ");
        }

        string token = CreateToken(user);

        return Ok(token);
    }

    [HttpPost("soft-delete/{username}")]
    public async Task<IActionResult> PerformSoftDelete(string username)
    {
        var softDeletedUser = await _repository.SoftDelete(username);
        return Ok(softDeletedUser);
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}