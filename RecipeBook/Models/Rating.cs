using System.Collections.Generic;

namespace RecipeBook.Models
{
  public class Rating
  {
    public Rating()
    {
      this.JoinEntries2 = new HashSet<RatingRecipe>();
    }
    public int RatingId { get; set; }
    public string StarRating { get; set; }
    public ICollection<RatingRecipe> JoinEntries2 { get; }
  }
}