using Corwords.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Corwords.Tests
{
    [TestClass]
    public class BaseTests
    {
        [TestMethod]
        public void TestHeading()
        {
            var currentBed = new TestBed("TestHeading");
            var cordoc = new Cordoc(currentBed.Markdown);
            Assert.AreEqual(cordoc.Html, currentBed.Html);
        }
    }
}
