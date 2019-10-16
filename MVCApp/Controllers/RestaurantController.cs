using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApp.Models;
using Data.Logic;

namespace MVCApp.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: Restaurant
        public ActionResult Index()
        {
            // index page for restaurant
            RestaurantModel model = QueryProcessor.GetFirstRestaurant<RestaurantModel>();
            return View(model);
        }

        [HttpGet]
        public JsonResult GetRestaurant(int ID )
        {
            // Method to get a restaurant when user clicks next/previous
            RestaurantModel model = QueryProcessor.GetRestaurantForID<RestaurantModel>((ID).ToString()); // Get the used for this ID

            if (model.RestaurantName == null) // If no result, return N/A to display error
            {
                return Json("N/A", JsonRequestBehavior.AllowGet);
            }
            if (model.RestaurantName == "") // If no result, return N/A to display error
            {
                return Json("N/A", JsonRequestBehavior.AllowGet);
            }

            return Json(model, JsonRequestBehavior.AllowGet); // If a result found, return it
        }
    }
}