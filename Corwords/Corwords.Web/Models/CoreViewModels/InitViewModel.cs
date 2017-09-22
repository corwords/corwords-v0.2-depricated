using Corwords.Web.Core.Integration;
using Corwords.Web.Models.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Corwords.Web.Models.CoreViewModels
{
    public class InitViewModel
    {
        public GeneralSettings GeneralSettings { get; set; }
        public List<string> Themes { get; set; }

        [Required, MaxLength(20)]
        public string FirstName { get; set; }
        [Required, MaxLength(20)]
        public string LastName { get; set; }
        [Required, EmailAddress, MaxLength(255)]
        public string EmailAddress { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
        [Required, Url]
        public string WebsiteUrl { get; set; }
        [Required, MaxLength(100)]
        public string SiteName { get; set; }
        [Required]
        public string SiteTheme { get; set; }
        [Required, MaxLength(255)]
        public string BlogName { get; set; }
        [Required, MinLength(2)]
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