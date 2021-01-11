namespace RecipeBook.Models
{
  public class RatingRecipe
  {
    public int RatingRecipeId { get; set; }
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    public int RatingId { get; set; }
    public Rating Rating { get; set; }
  }
}