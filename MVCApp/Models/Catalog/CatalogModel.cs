using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class CatalogModel
    {
        // data class used on the catalog index page to list the products under each category
        // No data annotations - not user facing
        public List<ProductModel> Products { get; set; }
        public List<CategoryModel> Categories { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter a search term")]
        [Display(Name ="Search Term")]
        public String SearchTerm { get; set; }
    }
}