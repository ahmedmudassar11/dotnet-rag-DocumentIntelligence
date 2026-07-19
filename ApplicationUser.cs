using Microsoft.AspNetCore.Identity;

namespace WebApplication1
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
