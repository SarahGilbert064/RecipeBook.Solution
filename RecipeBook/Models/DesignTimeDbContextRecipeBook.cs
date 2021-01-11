using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RecipeBook.Models
{
  public class RecipeBookContextFactory : IDesignTimeDbContextFactory<RecipeBookContext>
  {

    RecipeBookContext IDesignTimeDbContextFactory<RecipeBookContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<RecipeBookContext>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseMySql(connectionString);

      return new RecipeBookContext(builder.Options);
    }
  }
}