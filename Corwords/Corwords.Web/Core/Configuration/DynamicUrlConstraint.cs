using Corwords.Web.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;

namespace Corwords.Web.Core.Configuration
{
    public class DynamicUrlConstraint : IRouteConstraint
    {
        private readonly Func<CorwordsDbContext> _corwordsDbContext;

        public DynamicUrlConstraint(Func<CorwordsDbContext> corwordsDbContext)
        {
            _corwordsDbContext = corwordsDbContext;
        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values[routeKey] != null)
            {
                var dbContext = _corwordsDbContext();
                var url = "/" + values[routeKey].ToString();
                var dynamicRoute = dbContext.RouteFacts.FirstOrDefault(f => f.Url == url && (f.DateDiscontinued == null || f.DateDiscontinued >= DateTime.UtcNow));
                if (dynamicRoute != null)
                {
                    httpContext.Items["corwordsPage"] = dynamicRoute;
                    return true;
                }
            }
            return false;
        }
    }
}