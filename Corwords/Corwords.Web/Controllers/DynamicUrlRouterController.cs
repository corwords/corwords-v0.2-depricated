using Corwords.Web.Core.Types;
using Corwords.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Corwords.Web.Controllers
{
    public class DynamicUrlRouterController : Controller
    {
        public IActionResult Route(string corwords)
        {
            var page = HttpContext.Items["corwordsPage"] as RouteFact;
            //show the content with view

            switch (page.RouteType)
            {
                case DynamicRouteType.Blog:
                    return RedirectToAction("LatestPosts", "Blog", new { slug = page.Url });
                default:
                    break;
            }

            return View();
        }
    }
}