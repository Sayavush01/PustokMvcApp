using Microsoft.AspNetCore.Identity;

namespace MVC_WEB_APP.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }

    }
}
