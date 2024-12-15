namespace RecipesEverywhere.Model.DTO
{
    public class RecipeDTO
    {
        public int AuthorId { get; set; }
        
        public string? Text { get; set; }
        public string? Title { get; set; }
        
        public string? Picture { get; set; }
    }
}