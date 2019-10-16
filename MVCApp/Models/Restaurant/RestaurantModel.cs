using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MVCApp.Models
{
    public class RestaurantModel
    {
        // data class used to display the details of restaurants
        // Data annotations used - user facing class
        public int RestaurantID { get; set; }

        [Display(Name = "Restaurant Name")] // Set user-facing name of the field
        public string RestaurantName { get; set; }
        [Display(Name = "Address")]
        public string RestaurantAddress { get; set; }
        [Display(Name = "Cusine")]
        public string RestaurantDetails { get; set; }
        [Display(Name = "Website")]
        public string RestaurantURL { get; set; }

        [Display(Name = "Google Maps Link")]
        public string MapsSRC { get; set; }
    }
}