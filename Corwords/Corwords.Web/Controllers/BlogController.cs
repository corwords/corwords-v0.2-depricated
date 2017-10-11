using Corwords.Web.Core.Configuration;
using Corwords.Web.Models.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;

namespace Corwords.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IWritableOptions<GeneralSettings> _generalSettings;

        public BlogController(IOptions<AppSettings> appSettings, IWritableOptions<GeneralSettings> generalSettings)
        {
            _appSettings = appSettings;
            _generalSettings = generalSettings;
        }

        public IActionResult Rsd()
        {
            var homepage = _generalSettings.Value.WebsiteUrl;
            var metaweblog = _appSettings.Value.BlogSettings.MetaweblogEndpoint;

            var content = new StringBuilder();
            content.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            content.AppendLine("<rsd xmlns=\"" + homepage + "/blog/rsd\" version=\"1.0\">");
            content.AppendLine("<service>");
            content.AppendLine("<enginename>Corwords</enginename>");
            content.AppendLine("<enginelink>http://corwords.com/</enginelink>");
            content.AppendLine("<homepagelink>" + homepage + "</homepagelink>");
            content.AppendLine("<apis>");
            /// todo Enumerate blogs to get the various blogids
            content.AppendLine("<api name=\"MetaWeblog\" blogid=\"1\" preferred=\"true\" apilink=\"" + homepage + metaweblog + "\" />");
            content.AppendLine("</apis>");
            content.AppendLine("</service>");
            content.AppendLine("</rsd>");

            return Content(content.ToString(), "text/xml");
        }
    }
}