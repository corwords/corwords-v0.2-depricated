using Corwords.Web.Core.Types;
using System;

namespace Corwords.Web.Core.MVC
{
    public interface IRouteManager
    {
        int AddRoute(string url, DynamicRouteType routeType);
        int AddRoute(string url, string forwardTo, DynamicRouteType routeType);

        void RemoveRoute();
        void Refresh();
    }
}