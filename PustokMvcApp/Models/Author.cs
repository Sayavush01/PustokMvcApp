using PustokMvcApp.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace PustokMvcApp.Models
{
    public class Author:BaseEntity
    {
        [Required]    
        [MaxLength(25)]
        public string FullName { get; set; }
        public List<Book> Books { get; set; }
    }
}
