using Corwords.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

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
            Assert.AreEqual(currentBed.Html, cordoc.Html);
        }

        [TestMethod]
        public void TestYamlAndMarkdown()
        {
            var currentBed = new TestBed("YamlAndMarkdown");
            var cordoc = new Cordoc(currentBed.Markdown);
            Assert.AreEqual(currentBed.Html, cordoc.Html);
        }
    }
}
