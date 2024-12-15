using System.ComponentModel.DataAnnotations.Schema;

namespace RecipesEverywhere.Model
{
    [Table("Recipe")]
    public class RecipeModel
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }
        public string? Text { get; set; }

        public string? Title { get; set; }

        public byte[]? Picture { get; set; } 

        public int StatusId { get; set; } 
    }
}