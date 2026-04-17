using PustokMvcApp.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace PustokMvcApp.Models
{
    public class Tag:BaseEntity
    {
        [Required(ErrorMessage = "Tag name is required")]
        [StringLength(50, ErrorMessage = "Tag name cannot be more than 50 characters")]
        [Display(Name = "Tag Name")]
        public string Name { get; set; }
        public List<BookTag> BookTags { get; set; }
    }
}
