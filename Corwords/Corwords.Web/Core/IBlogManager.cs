using Corwords.Web.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace Corwords.Web.Core
{
    public interface IBlogManager
    {
        void CreateBlog(string name, string url, string username);
    }
}