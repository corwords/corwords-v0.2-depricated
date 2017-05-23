﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Corwords.Core;

namespace Corwords
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var md = "# Hello World!";

            var md = "---" +
                "Title: Hello World!" +
                "Author: jgaylord" +
                "---";

            var cordoc = new Cordoc(md);

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
