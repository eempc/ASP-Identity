using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the WebApplication1User class
    public class WebApplication1User : IdentityUser
    {
        // These two new fields are added here
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
    }
}
