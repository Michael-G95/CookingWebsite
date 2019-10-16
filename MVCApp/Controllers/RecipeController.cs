using Data.Logic;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApp.Controllers
{
    public class RecipeController : Controller
    {
        // GET: Recipe
        public ActionResult Index()
        {
            // Index page for recipe controller
            
            List<RecipeModel> recipeModels = QueryProcessor.GetRecipes<RecipeModel>(); // Get all recipes
            RecipeIndexModel model; // Model to be passed to view
            if (recipeModels.Count > 0)
            { // If any results returned for recipes
                int rand = new Random(DateTime.Now.Second).Next(0, recipeModels.Count); // Choose a random model index between 0 and the highest index (count-1)

                model = new RecipeIndexModel() // The model for the view
                {
                    Categories = QueryProcessor.GetMealCategories<MealCategoryModel>(),
                    Recipe = recipeModels[rand] // Choose a random recipe from the list
                };

            }
            else
            {
                model = new RecipeIndexModel() // The model for the view
                {
                    Categories = QueryProcessor.GetMealCategories<MealCategoryModel>()
                };
            }

            return View(model); // Pass model to view
        }

        public ActionResult Category(string CategoryName)
        {
            // Category action - view all recipes in a category given by parameter
            List<RecipeModel> model = QueryProcessor.GetRecipesForCategory<RecipeModel>(CategoryName);
            return View(model);
        }

        public ActionResult ViewRecipe(int RecipeID)
        {
            // ViewRecipe action - view a specific recipe given by ID parameter 
            RecipeViewModel model = new RecipeViewModel()
            {
                Ingredients = QueryProcessor.GetRecipeIngredientsForRecipeID<RecipeIngredientModel>(RecipeID),
                Methods = QueryProcessor.GetRecipeMethodForRecipeID<RecipeMethodModel>(RecipeID),
                RecipeInfo = QueryProcessor.GetRecipeForID<RecipeModel>(RecipeID.ToString())
            };
            return View(model);
        }
        
        public ActionResult SearchResult(string SearchText)
        {
            // Search for a specific recipe by name
            

            if (SearchText == null || SearchText.Length <1 ) // Check there is a query - client side validation failed but post sent anyway (noscript or manual post)
            {
                return RedirectToAction("Error", "Home", new ErrorModel { Message = "Error in your search - no term provided. Please enable javascript for the optimal site experience" });
            }
            else
            { // Invalid - throw user back to the home error page
                
                List<RecipeModel> model = QueryProcessor.SearchRecipesForName<RecipeModel>(SearchText);
                return View(model);
            }

        }
    }
}