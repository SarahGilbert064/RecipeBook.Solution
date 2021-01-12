using Microsoft.AspNetCore.Mvc;
using RecipeBook.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipeBook.Controllers
{
  public class CategoriesController : Controller
  {
    private readonly RecipeBookContext _db;
    public CategoriesController(RecipeBookContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List <Category> model = _db.Categories.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Category category)
    {
      _db.Categories.Add(category);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisCategory = _db.Categories
        .Include(category => category.JoinEntries)
        .ThenInclude(join => join.Recipe)
        .FirstOrDefault(category => category.CategoryId == id);
      return View(thisCategory);
    }

    public ActionResult Edit(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
      return View(thisCategory);
    }

    [HttpPost]
    public ActionResult Edit(Category category)
    {
      _db.Entry(category).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
      return View(thisCategory);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
      _db.Categories.Remove(thisCategory);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddRecipe(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "RecipeName");
      return View(thisCategory);
    }

    [HttpPost]
    public ActionResult AddRecipe(Recipe recipe, int CategoryId)
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
    
  }
}