using Microsoft.EntityFrameworkCore;
//Identifying the Database Schema

namespace RecipeBook.Models
{
  public class RecipeBookContext : DbContext
  {
    public virtual DbSet<Category> Categories { get; set; } //DBSets are new tables being created. 
    public DbSet<Recipe> Recipes { get; set; }

    public DbSet<CategoryRecipe> CategoryRecipe { get; set; }

    public RecipeBookContext(DbContextOptions options) : base(options) { } 
  }
}