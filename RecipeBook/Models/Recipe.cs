using System.Collections.Generic;

namespace RecipeBook.Models
{
  public class Recipe
  {
    public Recipe()
    {
      this.JoinEntries = new HashSet<CategoryRecipe>();
      this.JoinEntries2 = new HashSet<RatingRecipe>();
    }
    public int RecipeId { get; set; }
    public string RecipeName { get; set; }
    public string Description { get; set; }
    public ICollection<CategoryRecipe> JoinEntries { get; }
    public ICollection<RatingRecipe> JoinEntries2 { get; }
  }
}