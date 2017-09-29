using Corwords.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Corwords.Web.Data
{
    public class CorwordsDbContext : DbContext
    {
        public CorwordsDbContext(DbContextOptions<CorwordsDbContext> options) : base(options) { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var prefix = "Corwords_";

            builder.Entity<BlogTag>()
                .HasKey(k => k.BlogTagId);

            builder.Entity<BlogTag>()
                .HasOne(pt => pt.Blog)
                .WithMany(p => p.BlogTags)
                .HasForeignKey(fk => fk.BlogId);

            builder.Entity<BlogTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(p => p.BlogTags)
                .HasForeignKey(fk => fk.TagId);

            builder.Entity<Blog>().ToTable(prefix + "Blog");
            builder.Entity<BlogTag>().ToTable(prefix + "BlogTag");
            builder.Entity<Tag>().ToTable(prefix + "Tag");
        }

        public void TrashDatabaseSource()
        {
            this.Database.EnsureDeleted();
        }
    }
}