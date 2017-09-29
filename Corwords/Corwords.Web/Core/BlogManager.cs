using Corwords.Web.Data;
using Corwords.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Corwords.Web.Core
{
    public class BlogManager : IBlogManager
    {
        private readonly CorwordsDbContext _corwordsDbContext;

        public BlogManager(CorwordsDbContext corwordsDbContext)
        {
            _corwordsDbContext = corwordsDbContext;
        }

        public int CreateBlog(string name, string url, string username)
        {
            var blog = new Blog { Name = name, BaseUrl = url, Username = username };
            _corwordsDbContext.Blogs.Add(blog);
            _corwordsDbContext.SaveChanges();
            return blog.BlogId;
        }

        public bool ValidUserBlog(string username, int blogId)
        {
            return _corwordsDbContext.Blogs.Where(w => w.Username == username && w.BlogId == blogId).Any();
        }

        public List<Blog> GetBlogs(string username)
        {
            return _corwordsDbContext.Blogs.Where(w => w.Username == username).ToList();
        }

        public List<Tag> GetBlogTags(int blogId)
        {
            return _corwordsDbContext.Tags.Include(i => i.BlogTags.Select(s => s.BlogId == blogId)).ToList();
        }

        public Tag AddTag(string title, string description)
        {
            var dbTag = _corwordsDbContext.Tags.Where(w => w.Title.ToLower() == title.ToLower()).FirstOrDefault();

            if (dbTag == null)
            {
                var tag = new Tag { Title = title, Description = description };
                _corwordsDbContext.Add(tag);
                _corwordsDbContext.SaveChanges();
                dbTag = tag;
            }

            return dbTag;
        }

        public int AddBlogTag(int blogId, Tag tag)
        {
            var blog = _corwordsDbContext.Blogs.First(f => f.BlogId == blogId);

            var dbBlogTag = blog.BlogTags.FirstOrDefault(w => w.TagId == tag.TagId);

            if (dbBlogTag == null)
            {
                var blogTag = new BlogTag { Blog = blog, BlogId = blogId, Tag = tag, TagId = tag.TagId };
                blog.BlogTags.Add(blogTag);
                _corwordsDbContext.Update(blog);
                _corwordsDbContext.SaveChanges();
                dbBlogTag = blogTag;
            }

            return dbBlogTag.BlogTagId;
        }
    }
}