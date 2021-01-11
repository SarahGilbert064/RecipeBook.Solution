using System.Collections.Generic;

namespace RecipeBook.Models
{
  public class Category
  {
    public Category()
    {
      this.JoinEntries = new HashSet<CategoryRecipe>();
    }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public ICollection<CategoryRecipe> JoinEntries { get; set; }
  }
}