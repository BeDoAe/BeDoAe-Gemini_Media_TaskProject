using Microsoft.AspNetCore.Identity;

namespace TaskProject.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public List<UserTasks>? UserTasks { get; set; } // Join table for many-to-many relationship

    }
}
