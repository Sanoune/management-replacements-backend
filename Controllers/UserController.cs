
using System.Data;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
  [HttpGet]
  public IActionResult GetUsers(DatabaseHelper dbHelper)
  {
    var userService = new UserService(dbHelper);
    var users = userService.GetUsers();

    return Ok(users);
  }
}
