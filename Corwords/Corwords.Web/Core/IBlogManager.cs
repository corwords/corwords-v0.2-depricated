using Corwords.Web.Models;
using System;
using System.Collections.Generic;

namespace Corwords.Web.Core
{
    public interface IBlogManager
    {
        int CreateBlog(string name, string url, string username);
        bool ValidUserBlog(string username, int blogId);
        List<Blog> GetBlogs(string username);

        BlogPost AddPost(int blogId, string title, string body, DateTime dateCreated, string username);
        bool DeletePost(int postId);

        List<Tag> GetBlogTags(int blogId);
        Tag AddTag(string title, string description);
        int AddBlogTag(int blogId, Tag tag);
    }
}