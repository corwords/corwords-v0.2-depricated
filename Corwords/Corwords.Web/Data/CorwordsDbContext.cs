using Corwords.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Corwords.Web.Data
{
    public class CorwordsDbContext : DbContext
    {
        public CorwordsDbContext(DbContextOptions<CorwordsDbContext> options) : base(options) { }

        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var prefix = "Corwords_";

            builder.Entity<Blog>().ToTable(prefix + "Blog");
        }

        public void TrashDatabaseSource()
        {
            this.Database.EnsureDeleted();
        }
    }
}