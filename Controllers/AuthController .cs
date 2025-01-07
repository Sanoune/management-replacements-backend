using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        if (string.IsNullOrWhiteSpace(loginRequest.Username) || string.IsNullOrWhiteSpace(loginRequest.Password))
        {
            return BadRequest("Username and password are required.");
        }

        // Requête SQL pour vérifier les informations de connexion
        var query = "SELECT * FROM UsersTest WHERE name = @Username AND password = @Password";
        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", loginRequest.Username);
            command.Parameters.AddWithValue("@Password", loginRequest.Password);

            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    // Connexion réussie
                    return Ok(new { Message = "Login successful!" });
                }
                else
                {
                    // Échec de la connexion
                    return Unauthorized(new { Message = "Invalid username or password." });
                }
            }
        }
    }
}

public class LoginRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
