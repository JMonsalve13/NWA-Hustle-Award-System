using System;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace NWA.HustleCards.Persistance
{
    public class FileSave
    {
        private FileStream fs;

        public bool WriteFile(string path, string fileName, string data)
        {
            //Overwrite or create a CSV file from the folder structure
            //Returns if it successfully ran

            string fullPath = $"{path}\\{fileName}.CSV";
            if (!File.Exists(fullPath))
            {
                File.Create(fullPath);
            }
            else
            {
                File.WriteAllText(fullPath, data);
            }
            return true;
        }

        public bool TryRead(string path, out string CSV)
        {
            //Get the CSV from the folder structure and turn it into a string
            //Returns if it successfully ran
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
                CSV = Regex.Replace(CSV, @"\s+", "");
                return true;
            }
        }

        public bool AddImage(string fileName, byte[] photo)
        {
            //Get an image from the database and add it to the folder structure
            //Returns if it successfully ran
            string test = Convert.ToBase64String(photo);
            throw new NotImplementedException();
        }

        public void Save()
        {
            string path = Environment.ExpandEnvironmentVariables("%AppData%\\NWATemp");
            string savePath = Environment.ExpandEnvironmentVariables("%AppData%");
            Console.WriteLine(path);
            Console.WriteLine(savePath);

            //Zip up all the new files and overwrite the old file structure as a .NWA extension
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                
                ZipFile.CreateFromDirectory(path, savePath);
                Path.ChangeExtension(path + ".zip", ".NWA");
            }
            else
            {
                ZipFile.CreateFromDirectory(path, savePath);
                //Path.ChangeExtension(path + ".zip", ".NWA");
            }
        }
    }
}