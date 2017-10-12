using Corwords.Web.Core;
using Corwords.Web.Data;
using Corwords.Web.Models.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;

namespace Corwords.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IOptionsSnapshot<GeneralSettings> _generalSettings;
        private readonly CorwordsDbContext _corwordsDbContext;
        private readonly BlogManager _blogManager;

        public BlogController(IOptions<AppSettings> appSettings, 
                              IOptionsSnapshot<GeneralSettings> generalSettings,
                              CorwordsDbContext corwordsDbContext)
        {
            _appSettings = appSettings;
            _generalSettings = generalSettings;
            _corwordsDbContext = corwordsDbContext;
            _blogManager = new BlogManager(corwordsDbContext, generalSettings.Value);
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Rsd()
        {
            var homepage = _generalSettings.Value.WebsiteUrl;
            var metaweblog = _appSettings.Value.BlogSettings.MetaweblogEndpoint;
            var blogs = _blogManager.GetBlogs();

            var content = new StringBuilder();
            content.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            content.AppendLine("<rsd xmlns=\"" + homepage + "/blog/rsd\" version=\"1.0\">");
            content.AppendLine("<service>");
            content.AppendLine("<enginename>Corwords</enginename>");
            content.AppendLine("<enginelink>http://corwords.com/</enginelink>");
            content.AppendLine("<homepagelink>" + homepage + "</homepagelink>");
            content.AppendLine("<apis>");
            foreach(var blog in blogs)
                content.AppendLine("<api name=\"MetaWeblog\" blogid=\"" + blog.BlogId.ToString() + "\" preferred=\"true\" apilink=\"" + homepage + metaweblog + "\" />");
            content.AppendLine("</apis>");
            content.AppendLine("</service>");
            content.AppendLine("</rsd>");

            return Content(content.ToString(), "text/xml");
        }
    }
}