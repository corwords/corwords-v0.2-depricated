using Corwords.Web.Core.Types;
using Corwords.Web.Data;
using Corwords.Web.Models;
using System;

namespace Corwords.Web.Core.MVC
{
    public class RouteManager : IRouteManager
    {
        private readonly CorwordsDbContext _corwordsDbContext;

        public RouteManager(CorwordsDbContext corwordsDbContext)
        {
            _corwordsDbContext = corwordsDbContext;
        }

        public int AddRoute(string url, DynamicRouteType routeType)
        {
            return AddRoute(url, "", routeType);
        }

        public int AddRoute(string url, string forwardTo, DynamicRouteType routeType)
        {
            var routeFact = new RouteFact { Url = url, ForwardTo = forwardTo, DateCreated = DateTime.UtcNow, RouteType = routeType };
            _corwordsDbContext.RouteFacts.Add(routeFact);
            _corwordsDbContext.SaveChanges();
            return routeFact.RouteFactId;
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void RemoveRoute()
        {
            throw new NotImplementedException();
        }
    }
}