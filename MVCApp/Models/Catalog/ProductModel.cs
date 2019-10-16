using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MVCApp.Models
{
    public class ProductModel
    {
        // data class used on the index page and the searchresult page of catalog controller
        // data annotations provided - user-facing class

        [Display(Name ="Category Name")] // Set user-facing name of the field
        public string CategoryName{ get; set; }
        [Display(Name = "Product ID Number")]
        public int ProductID{ get; set; }
        [Display(Name = "Product Name")]
        public string ProdName { get; set; }
        [Display(Name = "Product Description")]
        public string ProdDescription_Short { get; set; }

        [RegularExpression(@"^\d+\.\d{0,2}$")] // Format price to 2 decimal places using regex
        [Range(0, 9999999999999999.99)]
        [Display(Name = "Product Price")]
        public decimal ProdPrice { get; set; }
    }
}