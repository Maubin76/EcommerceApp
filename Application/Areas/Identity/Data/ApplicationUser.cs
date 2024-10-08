using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Application.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    /* 
     * Override the property from IdentityUser class so the users do not have to confirm their email addresses.
     * Email confirmation may be implemented later once we found a way to manage it
     */
    public override bool EmailConfirmed { get; set; } = true;
}