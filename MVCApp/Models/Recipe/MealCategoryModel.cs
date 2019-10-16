using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class MealCategoryModel
    {

        // data class used in Recipe and Staff controllers, for a list of categories (ID and name)
        // No data annotations - not user facing

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}