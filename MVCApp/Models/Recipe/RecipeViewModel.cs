using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class RecipeViewModel
    {
        // data class used to hold information to display a recipe to user. 
        // No data annotations - not user facing (sub-classes are and are annotated)
        public RecipeModel RecipeInfo { get; set; }
        public List<RecipeMethodModel> Methods { get; set; }
        public List<RecipeIngredientModel> Ingredients { get; set; }
    }
}