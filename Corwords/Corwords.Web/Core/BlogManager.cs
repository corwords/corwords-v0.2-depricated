using Corwords.Web.Data;
using Corwords.Web.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public Task<EntityEntry<Blog>> CreateBlogAsync(string name, string url, string username)
        {
            return _corwordsDbContext.Blogs.AddAsync(new Blog { Name = name, BaseUrl = url, Username = username });
        }
    }
}