using Corwords.Web.Models;
using System.Collections.Generic;

namespace Corwords.Web.Core
{
    public interface IBlogManager
    {
        void CreateBlog(string name, string url, string username);

        List<Blog> GetBlogs(string username);
        List<Tag> GetBlogTags(int blogId);
    }
}