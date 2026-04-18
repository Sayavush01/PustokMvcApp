using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_WEB_APP.Models;
using PustokMvcApp.Models;

namespace PustokMvcApp.Data
{
    public class PustokMvcAppDbContext(DbContextOptions<PustokMvcAppDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookTag> BookTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Slider> Sliders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PustokMvcAppDbContext).Assembly);
            
        }
    }
}
