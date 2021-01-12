using System.Collections.Generic;

namespace RecipeBook.Models
{
  public class Recipe
  {
    public Recipe()
    {
      this.JoinEntries = new HashSet<CategoryRecipe>();
    }
    public int RecipeId { get; set; }
    public string RecipeName { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public int StarRating { get; set; }
    public ICollection<CategoryRecipe> JoinEntries { get; }
  }
}