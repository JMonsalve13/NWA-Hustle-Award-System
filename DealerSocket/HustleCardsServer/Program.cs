using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace HustleCardsServer
{
    /// <summary>
    /// Really just here, beacause C#'s Main's have to be in a class...
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Where the application starts
        /// </summary>
        /// <param name="args">Not in use... yet</param>
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
           //     .UseUrls("http://localhost:4200/")
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
