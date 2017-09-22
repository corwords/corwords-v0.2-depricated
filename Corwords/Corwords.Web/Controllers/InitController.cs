using Microsoft.AspNetCore.Mvc;
using Corwords.Web.Core.Configuration;
using Corwords.Web.Models.Configuration;

namespace Corwords.Web.Controllers
{
    public class InitController : Controller
    {
        private readonly IWritableOptions<GeneralSettings> _generalSettings;

        public InitController(IWritableOptions<GeneralSettings> generalSettings)
        {
            _generalSettings = generalSettings;
        }

        public IActionResult Index()
        {
            //_generalSettings.Update(vals =>
            //{
            //    vals.SiteName = "Corwords";
            //});

            return View(_generalSettings.Value);
        }
    }
}