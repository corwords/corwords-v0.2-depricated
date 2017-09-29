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

            // Add primary keys
            builder.Entity<Blog>()
                .HasKey(k => k.BlogId);

            builder.Entity<BlogPost>()
                .HasKey(k => k.BlogPostId);

            builder.Entity<BlogPostBlogTag>()
                .HasKey(k => k.BlogPostBlogTagId);

            builder.Entity<BlogTag>()
                .HasKey(k => k.BlogTagId);

            builder.Entity<Tag>()
                .HasKey(k => k.TagId);

            // Map foreign keys
            builder.Entity<BlogTag>()
                .HasOne(pt => pt.Blog)
                .WithMany(p => p.BlogTags)
                .HasForeignKey(fk => fk.BlogId);

            builder.Entity<BlogTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(p => p.BlogTags)
                .HasForeignKey(fk => fk.TagId);

            builder.Entity<BlogPostBlogTag>()
                .HasOne(pt => pt.BlogTag)
                .WithMany(p => p.BlogPostBlogTags)
                .HasForeignKey(fk => fk.BlogTagId);

            builder.Entity<BlogPostBlogTag>()
                .HasOne(pt => pt.BlogPost)
                .WithMany(p => p.BlogPostBlogTags)
                .HasForeignKey(fk => fk.BlogPostId);

            // Update table prefix
            builder.Entity<Blog>().ToTable(prefix + "Blog");
            builder.Entity<BlogPost>().ToTable(prefix + "BlogPost");
            builder.Entity<BlogPostBlogTag>().ToTable(prefix + "BlogPostBlogTag");
            builder.Entity<BlogTag>().ToTable(prefix + "BlogTag");
            builder.Entity<Tag>().ToTable(prefix + "Tag");
        }

        public void TrashDatabaseSource()
        {
            this.Database.EnsureDeleted();
        }
    }
}