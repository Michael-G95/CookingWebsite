using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class RecipeMethodModel
    {
        // data class used to hold the methods of a recipe, used for displaying to non-staff and used by staff to add methods.
        // Data annotations used - user facing class
        [Display(Name="Method Text")] // Setting user-facing name of field
        [MaxLength(255)] // max length to ensure can be stored in database 
        [Required(AllowEmptyStrings =false)] // required field
        public string MethodText { get; set; }
    }
}