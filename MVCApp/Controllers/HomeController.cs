using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.DataAccess;
using Data.Logic;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
          //Website home action - homepage
            return View();
        }

        [HttpGet]
        public void ImageJSON()
        {
            // Action to to pass the indexImages.json file to the page, allowing the image sliders to work
            Response.ContentType = "application/json";
            Response.TransmitFile(Server.MapPath("~/Content/indexImages.json")); // Transmit the file contents
        }

        public ActionResult About(int State = 0)
        {
            // Action to show the About page
            CommentModel m = new CommentModel()
            {
                State = State
            };
            return View(m);
        }
        public ActionResult Error(ErrorModel m)
        {
            // Action to show the Error page
            return View(m);
        }


        [ValidateInput(true)] // Validates input - incase of no javascript client-side validation
        [ValidateAntiForgeryToken] // Check for automated or fabricated post request 
        [HttpPost] // Method to handle http post
        public ActionResult SubmitComment(CommentModel m)
        {
            if (ModelState.IsValid) // Check for any model binding issues - client side validation failed but post sent anyway (noscript or manual post)
            {
                int State = QueryProcessor.InsertComment(m.CommentName, m.CommentEmail, m.CommentSubject, m.CommentMessage);
                return RedirectToAction("About",new {State});
            }
            else
            { // Invalid
                return RedirectToAction("Error",new ErrorModel { Message= "Error in your submitted comment - invalid fields! Please enable javascript for the best website experience." });
            }
            
        }

    }
}