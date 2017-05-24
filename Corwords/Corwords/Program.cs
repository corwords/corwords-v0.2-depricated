using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Corwords
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //// Basic inline test
            //var md = "---" + Environment.NewLine +
            //            "title: Introduction to Corwords" + Environment.NewLine +
            //            "author: jgaylord" + Environment.NewLine +
            //            "---" + Environment.NewLine +
            //            "# Introduction to Corwords" + Environment.NewLine +
            //            "" + Environment.NewLine +
            //            "By[Jason Gaylord](https://github.com/jgaylord)" + Environment.NewLine +
            //            "    " + Environment.NewLine +
            //            "### Getting Started" + Environment.NewLine +
            //            "" + Environment.NewLine +
            //            "There's 3 basic steps to get started:" + Environment.NewLine +
            //            "" + Environment.NewLine +
            //            "1.Step 1" + Environment.NewLine +
            //            "1.Step 2" + Environment.NewLine +
            //            "1.Step 3";

            //var cordoc = new Cordoc(md);

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
