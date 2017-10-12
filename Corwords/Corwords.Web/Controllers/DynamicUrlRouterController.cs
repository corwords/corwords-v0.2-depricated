using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Corwords.Web.Controllers
{
    public class DynamicUrlRouterController : Controller
    {
        public IActionResult Route()
        {
            return View();
        }
    }
}