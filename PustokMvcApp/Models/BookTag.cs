namespace PustokMvcApp.Models
{
    public class BookTag
    {
        public Book Book { get; set; }
        public Tag Tag { get; set; }
        public Guid BookId { get; set; }
        public Guid TagId { get; set; }
    }
}
