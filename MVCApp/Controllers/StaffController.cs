using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApp.Models;
using Data.Logic;

namespace MVCApp.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff Login Page
        public ActionResult Index(bool failedLogin = false,string ReturnUrl="")
        {
            // Index for staff - the login page
            // Accessible by all visitors to the site
            //2 optional parameters - used to display relevant errors 
            var model = new LoginModel() // Create the model to be used by the view 
            {
                FailedLogin = failedLogin, // If there has been a failed login, this optional param will be true
                Password = null,
                Username = null,
                AccessError = (ReturnUrl.Length > 0) ? true : false 
                    // If OWin throws a user to this page, it provides a ReturnURL param. If this is present, there is an invalid access error.
            };
            return View(model);
        }
    
        [Authorize(Roles ="Superadmin, Admin, Recipe, Product, Restaurant")] // Accessible by all logged in roless
        public ActionResult Menu()
        {
            // Display the staff menu
            return View();
        }

        [Authorize(Roles = "Superadmin, Admin, Recipe")] // Accessible by admins and the Recipe roles
        public ActionResult AddRecipe()
        {
            // Display the add recipe page
            var categories = QueryProcessor.GetMealCategories<MealCategoryModel>(); // Get all recipe categories
            var model = new AddRecipeModel // Create the model for the view
            {
                Recipe = null, // No recipe - this will be input by user 
                Categories = categories // List of categories
            };
            return View(model);
        }

        [Authorize(Roles = "Superadmin, Admin, Recipe")] // Accessible by admins and the Recipe roles
        [HttpPost] // POST only
        [ValidateAntiForgeryToken] // check for fabricated or automated post requests
        public ActionResult AddRecipe(AddRecipeModel model)
        {
            if (ModelState.IsValid) // Check for any model binding issues - client side validation failed but post sent anyway (noscript or manual post)
            {
                // if all OK then insert the recipe and sent to add recipe item page
                
                if(QueryProcessor.InsertRecipe(model.Recipe.RecipeTitle, model.Recipe.RecipeDescription, model.Recipe.RecipeCreator, model.Recipe.RecipePortions, model.Recipe.CategoryName) > 0)
                { // If rows affected > 0 no errors occoured in adding data
                    return RedirectToAction("AddRecipeItem");
                }
                else
                { // Display error message
                    return RedirectToAction("Error", new ErrorModel { Message = "Error in your submitted recipe - invalid fields! Please enable javascript for the best website experience." });
                }
                
            }
            else
            { // Invalid
                return RedirectToAction("Error", new ErrorModel { Message = "Error in your submitted recipe - invalid fields! Please enable javascript for the best website experience." });
            }
            
        }


        [Authorize(Roles = "Admin, Superadmin, Recipe")] // Accessible by admins and the Recipe roles
        public ActionResult AddRecipeItem(int resultcode = -1)
        {
            // Add a recipe method or ingredient page.
            // Param resultcode - used to display an error or success message.
            var model = new AddRecipeItemModel() // Create the model for the view
            {
                RecipeTitles = QueryProcessor.GetRecipes<RecipeModel>()
                    .Select(m => m.RecipeTitle).ToList(), //Get all the recipes, and select their titles as a list (to allow user to choose which recipe to edit),
               ResultCode=resultcode
            };
            return View(model);
        }

        [Authorize(Roles = "Admin, Superadmin, Recipe")]// Accessible by admins and the Recipe roles
        public ActionResult AddRecipeMethod(AddRecipeMethodModel model)
        {
            if (ModelState.IsValid) // Check for any model binding issues - client side validation failed but post sent anyway (noscript or manual post)
            {
                // if all OK then insert the method and sent to add recipe item page with the result code
                var result = QueryProcessor.InsertRecipeMethod(model.RecipeTitle, model.MethodText);
                return RedirectToAction(actionName: "AddRecipeItem", routeValues: new { resultcode = result });
            }
            else
            { // Invalid
                return RedirectToAction("Error", new ErrorModel { Message = "Error in your submitted method - invalid fields! Please enable javascript for the best website experience." });
            }

            
        }

        [Authorize(Roles = "Admin, Superadmin, Recipe")]// Accessible by admins and the Recipe roles
        public ActionResult AddRecipeIngredient(AddRecipeIngedientModel model)
        {
            if (ModelState.IsValid) // Check for any model binding issues - client side validation failed but post sent anyway (noscript or manual post)
            {
                // if all OK then insert the method and sent to add recipe item page with the result code
                var result = QueryProcessor.InsertRecipeIngredient(model.RecipeTitle, model.Ingredient.IngredientName, model.Ingredient.IngredientQuantity, model.Ingredient.IngredientUnit);
                return RedirectToAction(actionName: "AddRecipeItem", routeValues: new { resultcode = result });
            }
            else
            { // Invalid
                return RedirectToAction("Error", new ErrorModel { Message = "Error in your submitted ingredient - invalid fields! Please enable javascript for the best website experience." });
            }
            
        }


        [Authorize(Roles = "Superadmin, Admin, Recipe, Product, Restaurant")] // Accessible by any logged in user
        public ActionResult ViewComments()
        {
            // Comments page
            List<CommentModel> model;
            model = QueryProcessor.GetComments<CommentModel>(); // Get all comments from the database
            return View(model);
        }

        [Authorize(Roles ="Superadmin")] // Only accessible by superadmin role users
        public ActionResult CreateUser(int state = 0)
        {
            //optional param success used to display a message if account has been created successfully

            var model = new CreateLoginModel()
            {
                Roles = QueryProcessor.GetRoles<UserRoleModel>(), // Get all the possible roles from the database
                State = state
            };
            return View(model);
        }
        public ActionResult Error(ErrorModel m)
        {
            // return error page
            return View(m);
        }
    }

   
}