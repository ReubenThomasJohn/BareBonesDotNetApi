using BareBonesDotNetApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Entities;
using StudentApi.Repositories;
using BareBonesDotNetApi.Application.Services;

namespace BareBonesDotNetApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsCachedController : ControllerBase, IStudentsService
{

}