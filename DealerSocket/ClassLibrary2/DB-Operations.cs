using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace NWA.HustleCards.BackEnd
{
    class DB_Operations
    {


        public void StartDatabase()
        {
            using (var db = new LiteDatabase(@"C:\Temp\MyData.db")) ;
        }

        public void ShutdownDatabase()
        {

        }

        public void ExecuteCommand(string query)
        {


        }

        public bool AddCard(string query)
        {


            return true;
        }

        public bool AddPerson(string query)
        {


            return true;
        }


    }
}
