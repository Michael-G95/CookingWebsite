using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class AddRecipeModel
    {
        // data class used in Staff to add a recipe.
        // No data annotations - not user facing (subclasses are)
        public List<MealCategoryModel> Categories { get; set; }
        public RecipeModel Recipe { get; set;}
    }
}