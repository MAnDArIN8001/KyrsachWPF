using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecipesEverywhere.Model
{
    [Table("Users")]
    public class UserModel
    {
        [Key] [Column("Id")] public int UserId { get; set; }

        [Column("Name")] public string UserName { get; set; }
        [Column("Role")] public string Role { get; private set; } = "User";
        [Required, Column("PasswordHash")] public string PasswordHash { get; set; }
    }
}