using Microsoft.AspNet.Identity;
using System;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Data.Logic;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost] // POST only
        [ValidateAntiForgeryToken] // Security against fabricated/automated post requests
        public ActionResult Logon(LoginModel model) 
            // Method to log in a user, taking in username and password
        {
            if (!ModelState.IsValid) // Check for any model binding issues - client side validation failed but post sent anyway (noscript or manual post)
            {
                return RedirectToAction("Error", new ErrorModel { Message = "Error in logging in! Invalid details provided. Please enable javascript for an optimal site experience" });
            }
            if (QueryProcessor.ValidateLogin(model.Username, model.Password)){ // Check that a user exists with this password

                var claims = new List<Claim> // Create the list of claims - these are variables to be stored in the cookie authentication
                {
                    new Claim(ClaimTypes.NameIdentifier, "1"), // Add user ID  - this is not relevant for this website as Name is used for AntiForgeryToken as specified in global.cs
                    
                    new Claim(ClaimTypes.Name, model.Username), // Add username
                    new Claim(ClaimTypes.Role, QueryProcessor.GetUserRole<UserRoleModel>(model.Username).Role) 
                        // Take the user's role from the database and store in cookie - this will be used to access/deny to different staff pages
                        
                }; 

                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie); // Create a authentication cookie

                HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
                { // Add the cookie using OWin
                    AllowRefresh = true,
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(5) // Cookie expires 5 mins from login
                }, identity);
                return RedirectToAction("Menu", "Staff"); // Send to the staff menu
            }
            return RedirectToAction("Index", "Staff",new { failedLogin=true }); // Send back to login page (auth failed), display error message
        }

        public ActionResult Logoff()
            // Method to log off a user from the staff area
        {
            HttpContext.GetOwinContext()
                .Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie); // Delete the local authentication cookie
            return RedirectToAction("Index","Staff"); // Return to login page
        }

        [HttpPost] // POST only
        [ValidateAntiForgeryToken] // Check for automated or fabricated POST requests
        [Authorize(Roles ="Superadmin")] // Only accessible to users with Superadmin role
        public ActionResult Register(CreateLoginModel model)
        {
            if (!ModelState.IsValid) // Check for any model binding issues - client side validation failed but post sent anyway (noscript or manual post)
            {
                return RedirectToAction(actionName:"Error",controllerName:"Staff",routeValues: new ErrorModel { Message = "Error in creating the user! (Likely invalid data) Please ensure you enable javascript for an optimal site experience" });
            }
            var state = (QueryProcessor.InsertUser(model.Username, model.Password, model.Role));
                // Run the InsertUser query, check that 1 is returned (1 row affected)
                // If so, success is true (this will display a success message on the next page)
                // If not, success is false (this will display an error)
            return RedirectToAction("CreateUser", "Staff", new { state }); // Send to Staff/CreateUser page
        }
    }
}