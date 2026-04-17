using Microsoft.EntityFrameworkCore;
using PustokMvcApp.Models;

namespace PustokMvcApp.Data
{
    public class PustokMvcAppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookTag> BookTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public PustokMvcAppDbContext(DbContextOptions<PustokMvcAppDbContext> options) : base(options)
        {
        }
        protected  override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PustokMvcAppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
