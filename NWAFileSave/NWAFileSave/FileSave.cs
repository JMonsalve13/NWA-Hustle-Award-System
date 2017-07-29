using System;
using System.IO;
using System.IO.Compression;

namespace NWA.HustleCards.Persistance
{
    public class FileSave
    {
        private FileStream fs;

        public bool WriteFile(string fileName, string CSV)
        {
            //Overwrite or create a CSV file from the folder structure
            //Returns if it successfully ran
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }
            else
            {
                File.WriteAllText(fileName, CSV);
            }
            throw new NotImplementedException();
        }

        public bool TryRead(string fileName, out string CSV)
        {
            //Get the CSV from the folder structure and turn it into a string
            //Returns if it successfully ran
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("The file does not exist");
            }
            else
            {
                string test = File.ReadAllText(fileName);
            }
            throw new NotImplementedException();
        }

        public bool AddImage(string fileName, byte[] photo)
        {
            //Get an image from the database and add it to the folder structure
            //Returns if it successfully ran
            throw new NotImplementedException();
        }

        public void Save()
        {
            //Zip up all the new files and overwrite the old file structure as a .NWA extension
            if (!File.Exists("\\AppData\\Roaming\\"))
            {
                Directory.CreateDirectory("");
                ZipFile.CreateFromDirectory(/*Source*/"",/*Destination*/ ".MWA");
            }
            else
            {
                ZipFile.CreateFromDirectory(/*Source*/"",/*Destination*/ ".MWA");
            }
        }
    }
}