using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MVCApp.Models
{
    public class AddRecipeItemModel
    {
        // data class used in Staff controller to add a recipe detail - method or ingredient
        // Data annotations used - some properties are user facing 
        [Required(ErrorMessage = "Please choose a Recipe")] // Required field 
        [Display(Name ="Recipe Title")] // Set user-facing name of field
        public string RecipeTitle { get; set; }

        [Required(ErrorMessage = "Please enter some text for the method")]
        [Display(Name ="Method Text")]
        public string MethodText { get; set; }

        public RecipeIngredientModel Ingredient { get; set; }

        
        public List<string> RecipeTitles{ get; set; }

        public int ResultCode { get; set; }
    }
}