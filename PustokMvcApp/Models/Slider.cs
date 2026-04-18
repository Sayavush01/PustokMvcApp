using PustokMvcApp.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace PustokMvcApp.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string ButtonUrl { get; set; }
         public string ButtonText { get; set; }
        public string Description { get; set; }

        [NotMapped]
        [FileLength(2)]
        [FileTypes(".jpg, .jpeg, .png, .gif")]
        public IFormFile File { get; set; }
    }
}
