using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NWA.HustleCards.Persistance
{
    public class FileSave
    {
        public void CheckFileStructure(string path)
        {
            if (!File.Exists(System.AppContext.BaseDirectory + "\\NWATemp"))
            {
                Directory.CreateDirectory(System.AppContext.BaseDirectory + "\\NWATemp");
            }
            if (!File.Exists(System.AppContext.BaseDirectory + "\\NWATemp\\CSVFiles"))
            {
                Directory.CreateDirectory(System.AppContext.BaseDirectory + "\\NWATemp\\CSVFiles");
            }
            if (!File.Exists(System.AppContext.BaseDirectory + "\\NWATemp\\Images"))
            {
                Directory.CreateDirectory(System.AppContext.BaseDirectory + "\\NWATemp\\Images");
            }
        }

        public bool WriteFile(string path, string fileName, string data)
        {
            //Overwrite or create a CSV file from the folder structure
            //Returns if it successfully ran
            try
            {
                string fullPath = $"{path}\\{fileName}.CSV";
                if (!File.Exists(fullPath))
                {
                    File.Create(fullPath);
                    File.WriteAllText(fullPath, data);
                    return true;
                }
                else
                {
                    File.WriteAllText(fullPath, data);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool TryRead(string path, out string CSV)
        {
            //Get the CSV from the folder structure and turn it into a string
            //Returns if it successfully ran
            try
            {
                if (!File.Exists(path))
                {
                    CSV = "";
                    return false;
                }
                else
                {
                    CSV = "";
                    string[] lines = File.ReadAllLines(path);
                    for(int i = 0; i < lines.Length; i++)
                    {
                        if (i < lines.Length - 1)
                        {
                            CSV += lines[i] + ",";
                        }
                        else
                        {
                            CSV += lines[i];
                        }
                    }
                    //CSV = Regex.Replace(CSV, @"\s+", "");
                }
                return true;
            }
            catch (Exception)
            {
                CSV = "";
                return false;
            }
        }

        public bool AddImage(string path, string fileName, byte[] photo)
        {
            //Get an image from the database and add it to the folder structure
            //Returns if it successfully ran
            string test = Convert.ToBase64String(photo);
            throw new NotImplementedException();
        }

        public void Save(string data)
        {
            string path = System.AppContext.BaseDirectory + "\\NWATemp";
            Console.WriteLine(path);

            File.SetAttributes(path, FileAttributes.Normal);
            File.SetAttributes(System.AppContext.BaseDirectory + "\\NWATemp\\CSVFiles", FileAttributes.Normal);

            FileInfo FI = new FileInfo(System.AppContext.BaseDirectory);
            FI.IsReadOnly = false;

            XDocument xmlDoc = new XDocument();
            //XElement xEle = new XElement("People", from l in data.Split(',') select new XElement("Person", new XAttribute("Name", l)));
            string[] stuff = data.Split(',');
            XElement xEle = new XElement("People", 
                new XElement("Person", new XAttribute("ID", stuff[0]), new XAttribute("FirstName", stuff[1].Trim()), new XAttribute("LastName", stuff[2].Trim()), 
                new XAttribute("Email", stuff[3].Trim()), new XAttribute("Department", stuff[4].Trim()), new XAttribute("Location", stuff[5].Trim())));
            xmlDoc.Add(xEle);
            Console.WriteLine(xmlDoc);

            //if (!File.Exists(path + ".zip"))
            //{
            //    using (ZipArchive archive = ZipFile.Open(path + ".zip", ZipArchiveMode.Create))
            //    {
            //        FI.IsReadOnly = false;
            //        archive.CreateEntryFromFile(System.AppContext.BaseDirectory + "\\NWATemp\\CSVFiles", "CSVFiles");
            //        FI.IsReadOnly = false;
            //        archive.Dispose();
            //    }
            //}
            //else
            //{
            //    using (ZipArchive archive = ZipFile.Open(path + ".zip", ZipArchiveMode.Update))
            //    {
            //        FI.IsReadOnly = false;
            //        archive.CreateEntryFromFile(System.AppContext.BaseDirectory + "\\NWATemp\\CSVFiles", "CSVFiles");
            //        FI.IsReadOnly = false;
            //        archive.Dispose();
            //    }
            //}
        }
    }
}