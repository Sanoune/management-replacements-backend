using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<DatabaseHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/users", (DatabaseHelper dbHelper) =>
{
    string query = "SELECT * FROM UsersTest";

    try
    {
        var resultTable = dbHelper.ExecuteSelectQuery(query);

        var users = new List<object>();

        foreach (DataRow row in resultTable.Rows)
        {
            users.Add(new
            {
                Id = row["id"],
                Name = row["name"],
                Profil = row["profil"],
                InstitutionList = row["institutionList"],
                Password = row["password"],
                PasswordConfirm = row["passwordConfirm"]
            });
        }

        return Results.Ok(users);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erreur de connexion ou de requête : {ex.Message}");
    }
})
.WithName("GetUsers")
.WithOpenApi();


app.Run();
