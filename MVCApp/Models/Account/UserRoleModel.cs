using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class UserRoleModel
    {
        // Data class - used for logging in, creating users
        // No data annotations - not user facing
        public string Role { get; set; }
    }
}