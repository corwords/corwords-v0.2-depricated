using Corwords.Web.Data;
using Corwords.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using Corwords.Web.Models.Configuration;
using Corwords.Web.Core.Extensions;

namespace Corwords.Web.Core
{
    public class BlogManager : IBlogManager
    {
        private readonly CorwordsDbContext _corwordsDbContext;
        private readonly GeneralSettings _generalSettings;

        public BlogManager(CorwordsDbContext corwordsDbContext, GeneralSettings generalSettings)
        {
            _corwordsDbContext = corwordsDbContext;
            _generalSettings = generalSettings;
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
            var tags = _corwordsDbContext.Tags
                        .Include(i => i.BlogTags)
                            .Where(w => w.BlogTags.Any(a => a.BlogId == blogId)).ToList();

            return tags;
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

        public BlogPost AddPost(int blogId, string title, string body, DateTime dateCreated, string username)
        {
            var blog = _corwordsDbContext.Blogs.Include(i => i.BlogPosts).First(f => f.BlogId == blogId);

            // Scrub the slug and make it unique if it's not
            var slug = title.SlugEncode();

            var dbDuplicateSlug = blog.BlogPosts.FirstOrDefault(w => w.Slug == slug);
            if (dbDuplicateSlug != null)
                slug += "-" + dateCreated.Year.ToString() + "_" + dateCreated.Month.ToString() + "_" + dateCreated.Date.ToString();

            // Generate the permalink
            var permalink = _generalSettings.WebsiteUrl + blog.BaseUrl + slug;

            // Create the post
            var blogPost = new BlogPost { Author = username, BlogId = blogId, Body = body, DateCreated = dateCreated, Permalink = permalink, Slug = slug, Title = title };
            _corwordsDbContext.Add(blogPost);
            _corwordsDbContext.SaveChanges();

            return blogPost;
        }

        public List<BlogPost> GetLatestPosts(int blogId, int count)
        {
            return _corwordsDbContext.BlogPosts.Include(i => i.BlogPostTags).OrderByDescending(d => d.DateCreated).Take(count).ToList<BlogPost>();
        }

        public bool DeletePost(int postId)
        {
            var post = _corwordsDbContext.BlogPosts.FirstOrDefault(f => f.BlogPostId == postId);
            if (post == null)
                return false;
            post.DateDeleted = DateTime.UtcNow;

            _corwordsDbContext.Update(post);
            _corwordsDbContext.SaveChanges();
            return true;
        }
    }
}