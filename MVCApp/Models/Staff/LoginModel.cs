using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MVCApp.Models
{
    public class LoginModel
    {
        // data class used to log a user in.
        // Data annotations used - user facing class
        [Required(AllowEmptyStrings = false, ErrorMessage ="Please enter your username")]
        [Display(Name = "Username")]
        [StringLength(maximumLength: 20, ErrorMessage = "Usernames must be between 3 and 20 characters long", MinimumLength = 6)]
        //[RegularExpression(@"[^A-Za-z0-9]",ErrorMessage ="Usernames must only contain standard letters and numbers")] // Regular expression to rule out any non-letter or number character
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your password")]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 20,ErrorMessage ="Passwords must be between 6 and 20 characters long",MinimumLength =6)]
        //[RegularExpression(@"[^A-Za-z0-9]", ErrorMessage = "Passwords must only contain standard letters and numbers")]
        public string Password { get; set; }

        public bool FailedLogin { get; set; } // Used to display a message if a login attempt has been made, but failed

        public bool AccessError { get; set; } // Used to display a message if the user tried to access a page they don't have the correct role for (or aren't logged in)
    }
}