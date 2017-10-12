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

            return View();
        }
    }
}