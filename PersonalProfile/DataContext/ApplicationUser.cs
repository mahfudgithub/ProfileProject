using Microsoft.AspNetCore.Identity;

namespace PersonalProfile.DataContext
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
