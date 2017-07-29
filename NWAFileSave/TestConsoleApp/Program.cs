using System;
using NWA.HustleCards.Persistance;

namespace TestConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Program app = new Program();
            app.Run();
        }

        public void Run()
        {
            FileSave test = new FileSave();
            //test.Save();
            string fileName = "People";
            test.WriteFile(Environment.ExpandEnvironmentVariables("%Appdata%\\NWATemp\\"), fileName, "123456, Andrew, Sylvester, ASyl@Gmail.com, Sales, Salt Lake City");

            string holder = "";
            test.TryRead(Environment.ExpandEnvironmentVariables("%Appdata%\\NWATemp\\People.CSV"), out holder);
            Console.WriteLine(holder);
            
            test.WriteFile(Environment.ExpandEnvironmentVariables("%Appdata%\\NWATemp\\"), fileName, holder);
        }
    }
}