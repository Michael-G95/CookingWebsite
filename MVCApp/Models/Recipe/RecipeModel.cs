using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MVCApp.Models
{
    public class RecipeModel
    {


        // data class
        // No data annotations - not user facing
        // Data annotations used - user facing class
        public int RecipeID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Title!")] // Field required
        [Display(Name = "Recipe Title")] // Setting user-facing name of the field
        [MaxLength(30)] // Setting max length to ensure it can be stored in the database
        public string RecipeTitle { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Description!")]
        [Display(Name = "Recipe Description")]
        [MaxLength(120)]
        public string RecipeDescription { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Name!")]
        [Display(Name = "Recipe Creator")]
        [MaxLength(40)]
        public string RecipeCreator{ get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a Portion Number!")]
        [Display(Name = "Recipe Portions")]
        public int RecipePortions { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please choose a Category!")]
        [Display(Name = "Category Name")]
        [DataType(DataType.Text)]
        public string CategoryName { get; set; }

    }
}