using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Reflection;

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

        private struct UpdateValue<T>
        {
            public UpdateValue(string fiel, string val){
                field = fiel;
                value = val;
            }


            public string field;
            public string value;

            public T Update(T obj)
            {
                typeof(T).GetField(field).SetValue(obj, value);
                return obj;
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
                List<UpdateValue<Person>> ups = new List<UpdateValue<Person>>();

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
                        GenericQuery<Person> x = new GenericQuery<Person>(Query.EQ(column.Skip(column.Length - value.Length).ToString(), value), people);
                    }
                    else if (queryParams[i].Contains("|"))
                    {
                        GenericQuery<Person> x = new GenericQuery<Person>(Query.Contains(column, value), people);
                    }
                    else if (queryParams[i].Contains("+"))
                    {
                        ups.Add(new UpdateValue<Person>(column, value));
                    }
                }


                IEnumerable<Person> per = qlist[0].Run();

                for (int i = 1; i < qlist.Count; i++)
                {
                    IEnumerable<Person> peep = qlist[i].Run();
                    per = per.Where(x => peep.Contains(x));

                }

                if (ups.Count > 0)
                {
                    foreach (Person p in per)
                    {
                        Person q = p;
                        foreach (UpdateValue<Person> up in ups)
                        {
                            q = up.Update(q);
                        }
                        DeletePerson(p);
                        AddPerson(q);
                    }
                }

                return per.ToArray();
            };
        }
    }


    //     public Location[] GetLocation(string[] queryParams)
    //     {




    //   }

}
