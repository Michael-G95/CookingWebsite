using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class RecipeIngredientModel
    {

        // data class used to hold the ingredients of a recipe, used for displaying to non-staff and used by staff to add ingredients.
        // Data annotations used - user facing class
        [Required(ErrorMessage = "Please enter the ingredient name")]
        [MaxLength(20)] // Setting the max length so data can be stored in database
        [Display(Name ="Name")]
        public string IngredientName { get; set; }
        [Required(ErrorMessage = "Please enter the ingredient quantity")]
        [Display(Name = "Quantity")]
        public decimal IngredientQuantity{ get; set; }
        [Required(ErrorMessage = "Please enter the ingredient unit")]
        [MaxLength(10)]
        [Display(Name = "Unit")]
        public string IngredientUnit { get; set; }
    }
}