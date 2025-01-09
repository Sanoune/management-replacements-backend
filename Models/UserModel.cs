using System.ComponentModel.DataAnnotations; // Pour les annotations de validation
using System.Collections.Generic;
using System.Data; // Pour List<string>

namespace MyWebAPI.Models
{
  public class UserModel
  {
    // Les propriétés requises pour l'inscription de l'utilisateur
    [Required(ErrorMessage = "Le nom est requis.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "L'email est requis.")]
    [EmailAddress(ErrorMessage = "L'email n'est pas valide.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Le mot de passe est requis.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Le rôle est requis.")]
    public string Role { get; set; }

    [Required(ErrorMessage = "La liste des institutions est requise.")]
    public List<string> InstitutionList { get; set; } = new List<string>(); // Initialisation par défaut pour éviter null

    public UserModel(int id, string name, string email, string password, string role, string institutionList)
    {
      Name = name;
      Email = email;
      Password = password;
      Role = role;
    }


  }
}
