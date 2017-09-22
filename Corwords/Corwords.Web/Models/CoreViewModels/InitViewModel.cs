using Corwords.Web.Core.Integration;
using Corwords.Web.Models.Configuration;
using System.Collections.Generic;

namespace Corwords.Web.Models.CoreViewModels
{
    public class InitViewModel
    {
        public GeneralSettings GeneralSettings { get; set; }
        public List<string> Themes { get; set; }

        public InitViewModel(GeneralSettings generalSettings)
        {
            GeneralSettings = generalSettings;
            Themes = new Bootswatch().ThemeList();
        }
    }
}