using Corwords.Web.Models;
using System;
using System.Collections.Generic;

namespace Corwords.Web.Core
{
    public interface IBlogManager
    {
        int CreateBlog(string name, string url, string username);
        bool ValidUserBlog(string username, int blogId);
        List<Blog> GetBlogs();
        List<Blog> GetBlogs(string username);
        Blog GetBlog(string slug);

        BlogPost AddPost(int blogId, string title, string body, string[] tags, DateTime dateCreated, string username);
        bool DeletePost(int postId);
        List<BlogPost> GetLatestPosts(int blogId, int count);
        List<BlogPost> GetLatestPosts(string slug, int count);
        BlogPost GetPost(int postId);

        List<Tag> GetBlogTags(int blogId);
        Tag AddTag(string title, string description);
        int AddBlogTag(int blogId, Tag tag);
    }
}