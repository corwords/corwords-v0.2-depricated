using Corwords.Web.Models.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System;

namespace Corwords.Web.Core.Configuration
{
    public class FirstRunContraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var options = httpContext.RequestServices.GetService(typeof(IOptionsSnapshot<GeneralSettings>));
            return (options as IOptionsSnapshot<GeneralSettings>).Value.FirstRun;
        }
    }
}