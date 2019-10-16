using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
	public class CategoryModel
	{
        // data class used on the catalog index page to list the products under each category
        // No data annotations - not user facing
        public int CategoryID{ get; set; }
        public string CategoryName { get; set; }
    }
}