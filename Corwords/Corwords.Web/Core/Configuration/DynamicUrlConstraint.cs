using Corwords.Web.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;

namespace Corwords.Web.Core.Configuration
{
    public class DynamicUrlConstraint : IRouteConstraint
    {
        private readonly CorwordsDbContext _corwordsDbContext;

        public DynamicUrlConstraint(CorwordsDbContext corwordsDbContext)
        {
            _corwordsDbContext = corwordsDbContext;
        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values[routeKey] != null)
            {
                var url = "/" + values[routeKey].ToString();
                return _corwordsDbContext.RouteFacts.Any(a => a.Url == url && (a.DateDiscontinued == null || a.DateDiscontinued >= DateTime.UtcNow));
            }
            return false;
        }
    }
}