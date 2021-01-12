using Microsoft.AspNetCore.Mvc;
using RecipeBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace RecipeBook.Controllers
{
  public class RecipesController : Controller
  {
    private readonly RecipeBookContext _db;
    
    public RecipesController(RecipeBookContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Recipes.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "CategoryName");
      return View();
    }

    public ActionResult BestOf()
    {
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "RecipeName", "StarRating");
      return View(_db.Recipes.OrderByDescending(m=>m.StarRating).ToList());
    }

    // public ActionResult SearchBy()
    // {
    //   ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "RecipeName", "Ingredients");
    //   return View(_db.Recipes.OrderBy(m=>m.Ingredients).ToList());
    // }

//     public async Task<IActionResult> Index(string searchString)
// {
//     var movies = from m in _context.Movie
//                  select m;

//     if (!String.IsNullOrEmpty(searchString))
//     {
//         movies = movies.Where(s => s.Title.Contains(searchString));
//     }

//     return View(await movies.ToAsyncEnumerable().ToList()); // This line is different and does not require any additional using directives or packages to use
// }

    public async Task<IActionResult> SearchBy(string searchString)
    {
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "RecipeName", "Ingredients");
        var search = from m in _db.Recipes
          select m;

        if (!String.IsNullOrEmpty(searchString))
        {
            search = search.Where(s => s.Ingredients.Contains(searchString));
        }
      return View(await search.ToAsyncEnumerable().ToList()); // This line is different and does not require any additional using directives or packages to use
    }

    [HttpPost]
    public ActionResult Create(Recipe recipe, int CategoryId)
    {
      _db.Recipes.Add(recipe);
      if (CategoryId != 0)
      {
        var returnedJoin = _db.CategoryRecipe.Any(join => join.RecipeId == recipe.RecipeId && join.CategoryId == CategoryId);
        if (!returnedJoin) 
        {
          _db.CategoryRecipe.Add(new CategoryRecipe() { CategoryId = CategoryId, RecipeId = recipe.RecipeId });
        }
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisRecipe = _db.Recipes
        .Include(recipe => recipe.JoinEntries)
        .ThenInclude(join => join.Category)
        .FirstOrDefault(recipe => recipe.RecipeId == id);
      return View(thisRecipe);
    }

    public ActionResult Edit(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "CategoryName");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult Edit(Recipe recipe, int CategoryId)
    {
      if (CategoryId != 0)
      {
        var returnedJoin = _db.CategoryRecipe.Any(join => join.RecipeId == recipe.RecipeId && join.CategoryId == CategoryId);
        if (!returnedJoin) 
        {
          _db.CategoryRecipe.Add(new CategoryRecipe() { CategoryId = CategoryId, RecipeId = recipe.RecipeId });
        }
      }
      _db.Entry(recipe).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddCategory(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "CategoryName");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult AddCategory(Recipe recipe, int CategoryId)
    {
      if (CategoryId != 0)
      {
        var returnedJoin = _db.CategoryRecipe.Any(join => join.RecipeId == recipe.RecipeId && join.CategoryId == CategoryId); 
        if (!returnedJoin) 
        {
          _db.CategoryRecipe.Add(new CategoryRecipe() { CategoryId = CategoryId, RecipeId = recipe.RecipeId });
        }
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      return View(thisRecipe);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      _db.Recipes.Remove(thisRecipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteCategory(int joinId)
    {
      var joinEntry = _db.CategoryRecipe.FirstOrDefault(entry => entry.CategoryRecipeId == joinId);
      _db.CategoryRecipe.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
