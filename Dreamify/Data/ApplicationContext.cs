using Microsoft.EntityFrameworkCore;
using Dreamify.Models;
namespace Dreamify.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }

        public virtual DbSet<Genre> GetGenres() => Genres;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){ }
    }
}
