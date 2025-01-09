using System.Data;
using MyWebAPI.Models;

public class UserService
{
  private readonly DatabaseHelper _dbHelper;  // Singleton injected here

  // Injection du singleton DatabaseHelper via le constructeur
  public UserService(DatabaseHelper dbHelper)
  {
    _dbHelper = dbHelper;  // On l'assigne à un champ privé
  }

  public List<UserModel> GetUsers()
  {
    string query = "SELECT * FROM UsersTest";

    var resultTable = _dbHelper.ExecuteSelectQuery(query);
    var users = new List<UserModel>();

    foreach (DataRow row in resultTable.Rows)
    {
      users.Add(new UserModel((int)row["id"], (string)row["name"], (string)row["email"], (string)row["password"], (string)row["role"], (string)row["institutionList"]));
    }
    return users;
  }
}