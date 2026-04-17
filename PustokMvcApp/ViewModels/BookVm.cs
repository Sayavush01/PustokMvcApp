using PustokMvcApp.Models;

namespace PustokMvcApp.ViewModels
{
    public class BookVm
    {
        public Book Book { get; set; }
         public List<Book> RelatedBooks { get; set; }  
    }
}
