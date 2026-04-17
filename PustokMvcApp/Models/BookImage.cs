using PustokMvcApp.Models.Common;

namespace PustokMvcApp.Models
{
    public class BookImage:BaseEntity
    {
        public string Image { get; set; }
        public Book Book { get; set; }
        public Guid BookId { get; set; }

    }
}
