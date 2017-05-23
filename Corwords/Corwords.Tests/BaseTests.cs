using Corwords.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace Corwords.Tests
{
    [TestClass]
    public class BaseTests
    {
        [TestMethod]
        public void TestBasicHeading()
        {
            var currentBed = new TestBed("BasicHeading");
            var cordoc = new Cordoc(currentBed.Markdown);

            var expected = Regex.Replace(currentBed.Html, @"\r\n?|\n", "");
            var actual = Regex.Replace(cordoc.Html, @"\r\n?|\n", "");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestComplexMarkdown()
        {
            var currentBed = new TestBed("ComplexMarkdown");
            var cordoc = new Cordoc(currentBed.Markdown);

            var expected = Regex.Replace(currentBed.Html, @"\r\n?|\n", "");
            var actual = Regex.Replace(cordoc.Html, @"\r\n?|\n", "");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestYamlTitle()
        {
            var currentBed = new TestBed("BasicYaml");
            var cordoc = new Cordoc(currentBed.Markdown);
            Assert.AreEqual(cordoc.Title, "Introduction to Corwords");
        }

        [TestMethod]
        public void TestYamlAuthor()
        {
            var currentBed = new TestBed("BasicYaml");
            var cordoc = new Cordoc(currentBed.Markdown);
            Assert.AreEqual(cordoc.Author, "jgaylord");
        }
    }
}
