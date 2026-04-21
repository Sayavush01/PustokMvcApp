using PustokMvcApp.Models;
using PustokMvcApp.Models.Common;

namespace MVC_WEB_APP.Models
{
    public class BasketItem:BaseEntity
    {
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public int Count { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
