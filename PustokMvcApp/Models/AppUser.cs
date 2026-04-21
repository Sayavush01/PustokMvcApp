using Microsoft.AspNetCore.Identity;

namespace MVC_WEB_APP.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
            public List<BasketItem> BasketItems { get; set; }
            public List<Order> Orders { get; set; }
        public AppUser()
        {
            BasketItems = new List<BasketItem>();
            Orders = new List<Order>();
        }

    }
}
