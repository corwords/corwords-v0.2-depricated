﻿using Corwords.Web.Core.Types;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corwords.Web.Models
{
    public class RouteFact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RouteFactId { get; set; }
        public string Url { get; set; }
        public DynamicRouteType RouteType { get; set; }
        public string ForwardTo { get; set; }
    }
}