
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private static readonly List<string> tasks = new List<string>();

    [HttpGet]
    public IActionResult GetTasks()
    {
        return Ok(tasks);
    }

    [HttpPost]
    public IActionResult AddTask([FromBody] string task)
    {
        tasks.Add(task);
        return Ok(new { Message = "Task added successfully" });
    }

    [HttpGet("test-sql")]
    public IActionResult TestSql([FromServices] IConfiguration configuration)
    {
        var dbHelper = new DatabaseHelper(configuration);
        var result = dbHelper.ExecuteSelectQuery("SELECT TOP 5 * FROM dbo.UsersTest");
        return Ok(result);
    }

    [HttpDelete("{index}")]
    public IActionResult DeleteTask(int index)
    {
        if (index < 0 || index >= tasks.Count)
            return BadRequest("Invalid index");

        tasks.RemoveAt(index);
        return Ok(new { Message = "Task deleted successfully" });
    }
}
