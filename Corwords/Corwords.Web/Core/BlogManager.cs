using Corwords.Web.Data;
using Corwords.Web.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corwords.Web.Core
{
    public class BlogManager : IBlogManager
    {
        private readonly CorwordsDbContext _corwordsDbContext;

        public BlogManager(CorwordsDbContext corwordsDbContext)
        {
            _corwordsDbContext = corwordsDbContext;
        }

        public void CreateBlog(string name, string url, string username)
        {
            _corwordsDbContext.Blogs.Add(new Blog { Name = name, BaseUrl = url, Username = username });
            _corwordsDbContext.SaveChanges();
        }

        public List<Blog> GetBlogs(string username)
        {
            return _corwordsDbContext.Blogs.Where(w => w.Username == username).ToList();
        }
    }
}