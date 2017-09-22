using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Corwords.Web.Core.Integration
{
    public class Bootswatch
    {
        public List<string> ThemeList()
        {
            var files = Directory.EnumerateFiles("wwwroot\\css", "bootswatch.*.min.css").ToList();
            var themes = new List<string>();

            foreach (var file in files)
                themes.Add(file.Replace("wwwroot\\css\\bootswatch.", "").Replace(".min.css", ""));

            return themes;
        }
    }
}