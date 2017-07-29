using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace NWA.HustleCards.BackEnd
{
    class DB_Operations
    {

        public void AddPerson(Person p)
        {
            using (var db = new LiteDatabase("People.db"))
            {
                var people = db.GetCollection<Person>("People");
                // now we can carry out CRUD operations on the data

                people.Insert(new Person
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Department = p.Department,
                    Location = p.Location,
                    Email = p.Email,
                    ID = p.ID
                });

            }

        }

        public void DeletePerson(Person p)
        {

        }

        public void AddCard(HustleCard card)
        {


            using (var db = new LiteDatabase("HustleCards"))
            {
                var cards = db.GetCollection<HustleCard>("HustleCards");
                // now we can carry out CRUD operations on the data

                cards.Insert(new HustleCard
                {
                    PersonID = card.PersonID,
                    CardID = card.CardID,
                    PersonGiving = card.PersonGiving,
                    PersonReceiving = card.PersonReceiving,
                    PersonReceivingDepartment = card.PersonReceivingDepartment,
                    PersonReceivingLocation = card.PersonReceivingLocation,
                    Date = card.Date,
                    ReasonForCard = card.ReasonForCard
                });

            }
        }


        public void SelectAll()
        {

            using (var db = new LiteDatabase("HustleCards"))
            {
                var results = Query.All();
            }
        }


        private struct GenericQuery<T>
        {
            public Query q;
            public LiteCollection<T> t;
            public IEnumerable<T> Run()
            {
                return t.Find(q);
            }
            public GenericQuery(Query query, LiteCollection<T> col)
            {
                q = query;
                t = col;
            }

        }


        public Person[] GetPersons(string[] queryParams)
        {

            using (var db = new LiteDatabase("People.db"))
            {
                var people = db.GetCollection<Person>("People");
                List<GenericQuery<Person>> qlist = new List<GenericQuery<Person>>()
                {

                   new GenericQuery<Person>(Query.All(), people)
                };

                char val = 'a';
                string column = "";
                string value = "";

                for (int i = 0; i < queryParams.Length; i++)
                {

                    string[] columnAndVal = queryParams[i].Split('=', '<', '>', '|');
                    column = columnAndVal[0];
                    value = columnAndVal[1];

                    if (queryParams[i].Contains("="))
                    {
                        GenericQuery<Person> x = new GenericQuery<Person>(Query.EQ(column, value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains("<"))
                    {
                        GenericQuery<Person> x = new GenericQuery<Person>(Query.StartsWith(column, value), people);
                    }
                    else if (queryParams[i].Contains(">"))
                    {
                        GenericQuery<Person> x = new GenericQuery<Person>(Query.(column, value), people);
                    }
                    else if (queryParams[i].Contains("|"))
                    {
                        GenericQuery<Person> x = new GenericQuery<Person>(Query.Contains(column, value), people);
                    }
                    else if (queryParams[i].Contains("+"))
                    {
                        val = '+';
                    }






                    if (val.Equals('='))
                    {
                        // Exact

                    }
                    else if (val.Equals("<"))
                    {
                        // Start with
                        // personToAdd = people.FindOne();
                    }
                    else if (val.Equals(">"))
                    {
                        // Ends with
                        // personToAdd = people.FindOne();
                    }
                    else if (val.Equals("|"))
                    {
                        // Update
                        // personToAdd = people.FindOne();
                    }
                    else if (val.Equals("+"))
                    {

                    }
                }


                IEnumerable<Person> per = qlist[0].Run();

                for (int i = 1; i < qlist.Count; i++)
                {
                    IEnumerable<Person> peep = qlist[i].Run();
                    per = per.Where(x => peep.Contains(x));

                }
                return per.ToArray();
            };
        }
    }


    //     public Location[] GetLocation(string[] queryParams)
    //     {




    //   }

}
