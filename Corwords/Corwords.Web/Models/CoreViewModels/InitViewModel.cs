using Corwords.Web.Core.Integration;
using Corwords.Web.Models.Configuration;
using System.Collections.Generic;

namespace Corwords.Web.Models.CoreViewModels
{
    public class InitViewModel
    {
        public GeneralSettings GeneralSettings { get; set; }
        public List<string> Themes { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string WebsiteUrl { get; set; }
        public string SiteName { get; set; }
        public string SiteTheme { get; set; }
        public string BlogName { get; set; }
        public string BlogUrl { get; set; }

        public InitViewModel()
        {
            var GeneralSettings = new GeneralSettings();
            var Themes = new List<string>();
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(EmailAddress) && !string.IsNullOrEmpty(Password) &&
                !string.IsNullOrEmpty(WebsiteUrl) && !string.IsNullOrEmpty(SiteName) && !string.IsNullOrEmpty(SiteTheme) && !string.IsNullOrEmpty(BlogName) && !string.IsNullOrEmpty(BlogUrl);
        }

        public void ResolveNulls(GeneralSettings generalSettings, string currentUrl)
        {
            GeneralSettings = generalSettings;
            Themes = new Bootswatch().ThemeList();
            WebsiteUrl = WebsiteUrl ?? currentUrl;
            SiteName = SiteName ?? generalSettings.SiteName;
            BlogName = BlogName ?? "Blog";
            BlogUrl = BlogUrl ?? "blog";
        }
    }
}