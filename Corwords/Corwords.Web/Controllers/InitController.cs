using Microsoft.AspNetCore.Mvc;
using Corwords.Web.Core.Configuration;
using Corwords.Web.Models.Configuration;
using Corwords.Web.Models.CoreViewModels;

namespace Corwords.Web.Controllers
{
    public class InitController : Controller
    {
        private readonly IWritableOptions<GeneralSettings> _generalSettings;

        public InitController(IWritableOptions<GeneralSettings> generalSettings)
        {
            _generalSettings = generalSettings;
        }

        public IActionResult Begin(InitViewModel vm)
        {
            if (vm.IsValid())
            {
                //_generalSettings.Update(vals =>
                //{
                //    vals.SiteName = "Corwords";
                //});

                // Request came in from a post so let's save and move on.
                //return Content("Congrats!");
            }

            var currentUrl = Request.Scheme + "://" + Request.Host;
            vm.ResolveNulls(_generalSettings.Value, currentUrl);

            return View(vm);
        }
    }
}