using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class CreateLoginModel
    {
        // data class used in Staff to create a new user
        // Data annotations used - user facing class
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your username")] // Required field
        [Display(Name = "Username")] // Set user-facing name of field
        [StringLength(maximumLength: 20, ErrorMessage = "Usernames must be between 3 and 20 characters long", MinimumLength = 6)] // Set the length of string to meet password reqs. and to ensure can be stored in database
        //[RegularExpression(@"[^A-Za-z0-9]",ErrorMessage ="Usernames must only contain standard letters and numbers")] // Regular expression to rule out any non-letter or number character
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 20, ErrorMessage = "Passwords must be between 6 and 20 characters long", MinimumLength = 6)]
        //[RegularExpression(@"[^A-Za-z0-9]", ErrorMessage = "Passwords must only contain standard letters and numbers")]

        public string Password { get; set; }
        [Required(AllowEmptyStrings =false)]
        [MaxLength(10)]
        public string Role { get; set; }

        public List<UserRoleModel> Roles { get; set; }
        public int State { get; set; } // used to display a message if the user created successfully
    }
}