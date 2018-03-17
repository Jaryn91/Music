using Microsoft.EntityFrameworkCore;

namespace Musiction.API.Entities
{
    public class SongContext : DbContext
    {
        public SongContext(DbContextOptions<SongContext> options)
            : base(options)
        {
            Database.Migrate();
        }
        public DbSet<Song> Songs { get; set; }
    }
}
