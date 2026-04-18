using PustokMvcApp.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace PustokMvcApp.Models
{
    public class Author:BaseEntity
    {
        [Required(ErrorMessage = "Full Name is strictly required.")]    
        [MaxLength(25, ErrorMessage = "The Full Name cannot exceed 25 characters!")]
        public string FullName { get; set; }
        public List<Book> Books { get; set; }
    }
}
