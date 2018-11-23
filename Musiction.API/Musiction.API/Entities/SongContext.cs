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
        public DbSet<Presentation> Presentations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LinkSongToPresentation>().HasKey(k => new { PrresentationId = k.PresentationId, k.SongId });

            modelBuilder.Entity<LinkSongToPresentation>()
                .HasOne(x => x.Song)
                .WithMany(x => x.LinkSongToPresentation)
                .HasForeignKey(x => x.SongId);

            modelBuilder.Entity<LinkSongToPresentation>()
                .HasOne(x => x.Presentation)
                .WithMany(x => x.LinkSongToPresentation)
                .HasForeignKey(x => x.PresentationId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
