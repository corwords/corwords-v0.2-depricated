using System.IO;
using System.Reflection;

namespace Corwords.Tests
{
    public class TestBed
    {
        public string Markdown { get; set; }
        public string Html { get; set; }

        private string _TestBedFolder()
        {
            var baseDirectory = Directory.GetCurrentDirectory();
            /// TODO Fix the base directory
            return baseDirectory + "../../../../" + "/TestBedFiles/";
        }

        public TestBed(string testName)
        {
            Markdown = File.ReadAllText(_TestBedFolder() + testName + "/" + testName + ".md");
            Html = File.ReadAllText(_TestBedFolder() + testName + "/" + testName + ".htm");
        }
    }
}