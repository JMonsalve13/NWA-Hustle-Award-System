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
            test.CheckFileStructure(System.AppContext.BaseDirectory);
            
            string writepath = System.AppContext.BaseDirectory + "\\NWATemp\\CSVFiles";
            string imagePath = System.AppContext.BaseDirectory + "\\NWATemp\\Images";

            //test.AddImage(imagePath, "Teddybear", );

            //string fileName = "People";
            //bool success = test.WriteFile(writepath, fileName, "1, Andrew, Sylvester, ASyl@Gmail.com, Sales, Salt Lake City");

            string holder = "";
            test.TryRead(Environment.ExpandEnvironmentVariables(writepath + "\\People.CSV"), out holder);
            Console.WriteLine(holder);
            Console.WriteLine();

            //fileName = "Prizes";
            //success = test.WriteFile(Environment.ExpandEnvironmentVariables(writepath), fileName, "1, TeddyBear, 1.50, true, Fluffy and clean, C:/temp");

            //holder = "";
            //success = test.TryRead(Environment.ExpandEnvironmentVariables(writepath + "\\Prizes.CSV"), out holder);
            //Console.WriteLine(holder);
            //Console.WriteLine();

            //fileName = "Cards";
            //success = test.WriteFile(Environment.ExpandEnvironmentVariables(writepath), fileName, "1, 342, 254, Sales, Draper, 07-29-17, Good Person");

            //holder = "";
            //success = test.TryRead(Environment.ExpandEnvironmentVariables(writepath + "\\Cards.CSV"), out holder);
            //Console.WriteLine(holder);
            //Console.WriteLine();

            //fileName = "Departments";
            //success = test.WriteFile(Environment.ExpandEnvironmentVariables(writepath), fileName, "Sales, Purchasing, HR");

            //holder = "";
            //success = test.TryRead(Environment.ExpandEnvironmentVariables(writepath + "\\Departments.CSV"), out holder);
            //Console.WriteLine(holder);
            //Console.WriteLine();

            //fileName = "Locations";
            //success = test.WriteFile(Environment.ExpandEnvironmentVariables(writepath), fileName, "Draper, Salt Lake City, Russia");

            //holder = "";
            //success = test.TryRead(Environment.ExpandEnvironmentVariables(writepath + "\\Locations.CSV"), out holder);
            //Console.WriteLine(holder);
            //Console.WriteLine();

            test.Save(holder);
        }
    }
}