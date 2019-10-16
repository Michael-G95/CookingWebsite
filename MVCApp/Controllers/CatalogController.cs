using Data.Logic;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApp.Controllers
{
    public class CatalogController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            // Index action for catalog controller

            CatalogModel c = new CatalogModel // Model for the view to render
            {
                Products = QueryProcessor.GetProductsByCategory<ProductModel>(), // Get the products which will be displayed
                Categories = QueryProcessor.GetCategories<CategoryModel>() // Get the categories which will be displayed
            }; 

            return View(c); // Pass in the CatalogModel to the view
        }

        [HttpGet]
        public ActionResult SearchByName(string SearchTerm)
            //Function to search by category- pass to searchresult page with appropriate type
        {
            return RedirectToAction("SearchResult", new { type = 0, SearchTerm });
        }

        [HttpGet]
        public ActionResult SearchByCategory(string SearchTerm)
            //Function to search by category- pass to searchresult page with appropriate type
        {
            return RedirectToAction("SearchResult", new { type = 1, SearchTerm });
        }

        public ActionResult SearchResult(int type,string SearchTerm)
        { // Parameters: type = type of search, SearchTerm = value to search for 

            if(SearchTerm == null|| SearchTerm.Length < 1)
            {
                return RedirectToAction("Error", "Home", new ErrorModel { Message = "Error in your search - no term provided. Please enable javascript for the optimal site experience" });
            }
            List<ProductModel> model = null; // null returned if the type is invalid

            if (type == 0) // type 0 - search by name
            {
                model = QueryProcessor.GetProductsForName<ProductModel>(SearchTerm); // Search for the name
            }else if (type == 1) // type 1 - search by category
            {
                model = QueryProcessor.GetProductsForCategory<ProductModel>(SearchTerm); // Search for the category
            }
            
            return View(model);
        }

        public ActionResult Product(int ProductID)
        { // Parameter ID = the product ID to display
            // Action to display a single product 
            ProductDisplayModel model = QueryProcessor.GetProductForID<ProductDisplayModel>(ProductID.ToString());
                // Get the product for the parameter ID
            return View(model);
        }
    }
}