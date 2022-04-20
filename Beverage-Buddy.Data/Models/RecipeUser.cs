using Microsoft.AspNetCore.Identity;

namespace Beverage_Buddy.Data.Models
{
    public class RecipeUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
