using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class RecipeIndexModel
    {

        // data class used on the index page of the Recipe controller to display products and categories
        // No data annotations - not user facing
        public RecipeModel Recipe { get; set; }
        public List<MealCategoryModel> Categories {get;set;}

        [Required(AllowEmptyStrings =false,ErrorMessage ="Please provide a search term!")]
        public string SearchText { get; set; }
    }
}