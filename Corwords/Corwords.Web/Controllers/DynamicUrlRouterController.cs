using Corwords.Web.Core;
using Corwords.Web.Core.Types;
using Corwords.Web.Data;
using Corwords.Web.Models;
using Corwords.Web.Models.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Corwords.Web.Controllers
{
    public class DynamicUrlRouterController : Controller
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IOptionsSnapshot<GeneralSettings> _generalSettings;
        private readonly CorwordsDbContext _corwordsDbContext;
        private readonly BlogManager _blogManager;

        public DynamicUrlRouterController(IOptions<AppSettings> appSettings,
                              IOptionsSnapshot<GeneralSettings> generalSettings,
                              CorwordsDbContext corwordsDbContext)
        {
            _appSettings = appSettings;
            _generalSettings = generalSettings;
            _corwordsDbContext = corwordsDbContext;
            _blogManager = new BlogManager(corwordsDbContext, generalSettings.Value);
        }

        public IActionResult Route(string corwords)
        {
            var page = HttpContext.Items["corwordsPage"] as RouteFact;
            //show the content with view

            switch (page.RouteType)
            {
                case DynamicRouteType.Blog:
                    var latestPosts = _blogManager.GetLatestPosts(page.Url, 10);
                    return View("~/Views/Blog/LatestPosts.cshtml", latestPosts);
                default:
                    break;
            }

            return View();
        }
    }
}