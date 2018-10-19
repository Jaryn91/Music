using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Musiction.API.Entities;

namespace Musiction.API.Migrations
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SongContext>
    {
        public SongContext CreateDbContext(string[] args)
        {
            //var connectionString =


            var builder = new DbContextOptionsBuilder<SongContext>();
            builder.UseMySql(connectionString);
            return new SongContext(builder.Options);
        }
    }
}
