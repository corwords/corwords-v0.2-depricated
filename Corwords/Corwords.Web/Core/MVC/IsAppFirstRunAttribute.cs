using Corwords.Web.Models.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace Corwords.Web.Core.MVC
{
    public class IsAppFirstRunAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var options = filterContext.HttpContext.RequestServices.GetService(typeof(IOptionsSnapshot<GeneralSettings>));
            var firstRun = (options as IOptionsSnapshot<GeneralSettings>).Value.FirstRun;

            if (!firstRun)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index"
                }));
            }
        }
    }
}