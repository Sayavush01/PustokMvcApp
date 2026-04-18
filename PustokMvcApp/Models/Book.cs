using PustokMvcApp.Attributes;
using PustokMvcApp.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokMvcApp.Models
{
    public class Book:BaseEntity
    {
        [Required(ErrorMessage = "Book Name is strictly required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Description is strongly required")]
        public string Description { get; set; }
        public int DiscountPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public Author Author { get; set; }
        public Guid AuthorId { get; set; }
        public string MainImageUrl { get; set; }
        public string HoverImageUrl { get; set; }
        public bool IsNew { get; set; }
        public bool IsFeatured { get; set; }
        public bool InStock { get; set; }
        public int Code { get; set; }
        public List<BookImage> BookImages { get; set; }
        public List<BookTag> BookTags { get; set; }


        [NotMapped]
        public List<Guid> TagIds { get; set; }
        [NotMapped]
        [FileLength(2)]
        [FileTypes(".jpg, .jpeg, .png")]

        public List<IFormFile> Files { get; set; }
        [NotMapped]
        [FileLength(2)]
        [FileTypes(".jpg, .jpeg, .png")]

        public IFormFile MainPhoto { get; set; }
        [NotMapped]
        [FileLength(2)]
        [FileTypes(".jpg, .jpeg, .png")]

        public IFormFile HoverPhoto { get; set; }
        public Book()
        {
            BookImages = [];
            BookTags = [];
                
        }
    }
}
