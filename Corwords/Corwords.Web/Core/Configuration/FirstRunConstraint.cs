using Corwords.Web.Models.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace Corwords.Web.Core.Configuration
{
    public class FirstRunContraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var options = httpContext.RequestServices.GetService(typeof(IOptions<AppSettings>));
            return (options as IOptions<AppSettings>).Value.FirstRun;
        }
    }
}