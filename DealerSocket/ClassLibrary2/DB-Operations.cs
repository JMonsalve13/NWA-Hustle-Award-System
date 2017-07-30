using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Reflection;

namespace NWA.HustleCards.BackEnd
{
    public static class DB_Operations
    {
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
        
        /// <summary>
        /// AddCard() will connect to the HustleCard DB and insert the card passed in, into the DB
        /// </summary>
        /// <param name="card"></param>
        public static void AddCard(HustleCard card)
        {
            using (var db = new LiteDatabase("HustleCards.db"))
            {
                var cards = db.GetCollection<HustleCard>("HustleCards");
                
                cards.Insert(HustleCard.Clone(card));
            }
        }
        /// <summary>
        /// DeleteCard() will connect to the HustCard DB and delete the card specified. 
        /// </summary>
        /// <param name="p"></param>
        public static void DeleteCard(HustleCard p)
        {
            using (var db = new LiteDatabase("HustleCards.db"))
            {
                var cards = db.GetCollection<Department>("HustleCards");
         //       cards.Delete(p);
            }
        }
        /// <summary>
        ///     In GetCards(), passing in a string array of queries.
        ///     
        ///     We create a new list of type <GenericQuery<HustleCard>> named qlist that stores all
        ///     the queries that eventually gets populated and executed.
        ///     
        ///     Following, we then create a new list of type <GenericQuery<HustleCard>> named ups, in which stores all of the update
        ///     queries that also eventually gets populated and executed,
        ///     
        ///     Following, the column and value variables get declared ouside of the for loop so they dont get re-declared
        ///     with in every itteration of the for loop.
        ///     
        ///     In the for loop, we are itteratinng through every index in the queryParams[], depending on the query in the current index refrence,
        ///     we then add that query to qlist to add the execution at the end of the for loop.
        ///     
        ///     We then itterate through the qlist and execute updates and queries.
        ///     
        /// 
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns>HustleCard[]</returns>
        public static HustleCard[] GetCards(string[] queryParams)
        {

            using (var db = new LiteDatabase("HustleCards.db"))
            {
                var people = db.GetCollection<HustleCard>("Cards");
                List<GenericQuery<HustleCard>> qlist = new List<GenericQuery<HustleCard>>()
                {

                   new GenericQuery<HustleCard>(Query.All(), people)
                };
                List<UpdateValue<HustleCard>> ups = new List<UpdateValue<HustleCard>>();

                string column = "";
                string value = "";

                for (int i = 0; i < queryParams.Length; i++)
                {

                    string[] columnAndVal = queryParams[i].Split('=', '<', '>', '|');
                    column = columnAndVal[0];
                    value = columnAndVal[1];

                    if (queryParams[i].Contains("="))
                    {
                        GenericQuery<HustleCard> x = new GenericQuery<HustleCard>(Query.EQ(column, value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains("<"))
                    {
                        GenericQuery<HustleCard> x = new GenericQuery<HustleCard>(Query.StartsWith(column, value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains(">"))
                    {
                        GenericQuery<HustleCard> x = new GenericQuery<HustleCard>(Query.EQ(column.Skip(column.Length - value.Length).ToString(), value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains("|"))
                    {
                        GenericQuery<HustleCard> x = new GenericQuery<HustleCard>(Query.Contains(column, value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains("+"))
                    {
                        ups.Add(new UpdateValue<HustleCard>(column, value));
                    }
                }


                IEnumerable<HustleCard> per = qlist[0].Run();

                for (int i = 1; i < qlist.Count; i++)
                {
                    IEnumerable<HustleCard> peep = qlist[i].Run();
                    per = per.Where(x => peep.Contains(x));
                }

                if (ups.Count > 0)
                {
                    foreach (HustleCard p in per)
                    {
                        HustleCard q = p;
                        foreach (UpdateValue<HustleCard> up in ups)
                        {
                            q = up.Update(q);
                        }
                        DeleteCard(p);
                        AddCard(q);
                    }
                }

                return per.ToArray();
            };
        }
       
        /// <summary>
        /// AddDepartment() will connect to Department DB and insert the Department passed in, into the DB
        /// </summary>
        /// <param name="card"></param>
        public static void AddDepartment(Department p)
        {
            using (var db = new LiteDatabase("Departments.db"))
            {
                var people = db.GetCollection<Department>("Departments");
                // now we can carry out CRUD operations on the data

                people.Insert(Department.Clone(p));
            }
        }
        /// <summary>
        /// DeleteDepartment() will connect to the the departments database and remove the specified department
        /// </summary>
        /// <param name="p"></param>
        public static void DeleteDepartment(Department p)
        {
            using(var db = new LiteDatabase("Departments.db"))
            {
                var departments = db.GetCollection<Department>("Departments");
       //         departments.Delete(p);
            }
        }
        /// <summary>
        ///     In GetDepartments(), passing in a string array of queries.
        ///     
        ///     We create a new list of type <GenericQuery<Department>> named qlist that stores all
        ///     the queries that eventually gets populated and executed.
        ///     
        ///     Following, we then create a new list of type <GenericQuery<Department>> named ups, in which stores all of the update
        ///     queries that also eventually gets populated and executed,
        ///     
        ///     Following, the column and value variables get declared ouside of the for loop so they dont get re-declared
        ///     with in every itteration of the for loop.
        ///     
        ///     In the for loop, we are itteratinng through every index in the queryParams[], depending on the query in the current index refrence,
        ///     we then add that query to qlist to add the execution at the end of the for loop.
        ///     
        ///     We then itterate through the qlist and execute updates and queries.
        ///     
        /// 
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns>Department[]</returns>
        public static Department[] GetDepartments(string[] queryParams)
        {

            using (var db = new LiteDatabase("Departments.db"))
            {
                var people = db.GetCollection<Department>("Departments");
                List<GenericQuery<Department>> qlist = new List<GenericQuery<Department>>()
                {

                   new GenericQuery<Department>(Query.All(), people)
                };
                List<UpdateValue<Department>> ups = new List<UpdateValue<Department>>();

                string column = "";
                string value = "";

                for (int i = 0; i < queryParams.Length; i++)
                {

                    string[] columnAndVal = queryParams[i].Split('=', '<', '>', '|');
                    column = columnAndVal[0];
                    value = columnAndVal[1];

                    if (queryParams[i].Contains("="))
                    {
                        GenericQuery<Department> x = new GenericQuery<Department>(Query.EQ(column, value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains("<"))
                    {
                        GenericQuery<Department> x = new GenericQuery<Department>(Query.StartsWith(column, value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains(">"))
                    {
                        GenericQuery<Department> x = new GenericQuery<Department>(Query.EQ(column.Skip(column.Length - value.Length).ToString(), value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains("|"))
                    {
                        GenericQuery<Department> x = new GenericQuery<Department>(Query.Contains(column, value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains("+"))
                    {
                        ups.Add(new UpdateValue<Department>(column, value));
                    }
                }


                IEnumerable<Department> per = qlist[0].Run();

                for (int i = 1; i < qlist.Count; i++)
                {
                    IEnumerable<Department> peep = qlist[i].Run();
                    per = per.Where(x => peep.Contains(x));
                }

                if (ups.Count > 0)
                {
                    foreach (Department p in per)
                    {
                        Department q = p;
                        foreach (UpdateValue<Department> up in ups)
                        {
                            q = up.Update(q);
                        }
                        DeleteDepartment(p);
                        AddDepartment(q);
                    }
                }

                return per.ToArray();
            };
        }
        /// <summary>
        /// AddPrize() will connect to the prizes db and add a prize based on what prize is being passed in th
        /// </summary>
        /// <param name="p"></param>
        public static void AddPrize(Prize p)
        {
            using (var db = new LiteDatabase("Prizes.db"))
            {
                var people = db.GetCollection<Prize>("Prizes");
                // now we can carry out CRUD operations on the data

                people.Insert(Prize.Clone(p));
            }
        }
        /// <summary>
        /// DeletePrize() will connect to the prizes database and delete the specified prize
        /// </summary>
        /// <param name="p"></param>
        public static void DeletePrize(Prize p)
        {
            using (var db = new LiteDatabase("Prizes.db"))
            {
                var prizes = db.GetCollection<Department>("Prizes");
         //       prizes.Delete(p);
            }
        }
        /// <summary>
        ///     In GetPrizes(), passing in a string array of queries.
        ///     
        ///     We create a new list of type <GenericQuery<prize>> named qlist that stores all
        ///     the queries that eventually gets populated and executed.
        ///     
        ///     Following, we then create a new list of type <GenericQuery<prize>> named ups, in which stores all of the update
        ///     queries that also eventually gets populated and executed,
        ///     
        ///     Following, the column and value variables get declared ouside of the for loop so they dont get re-declared
        ///     with in every itteration of the for loop.
        ///     
        ///     In the for loop, we are itteratinng through every index in the queryParams[], depending on the query in the current index refrence,
        ///     we then add that query to qlist to add the execution at the end of the for loop.
        ///     
        ///     We then itterate through the qlist and execute updates and queries.
        ///     
        /// 
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns>Prize[]</returns>
        public static Prize[] GetPrizes(string[] queryParams)
        {

            using (var db = new LiteDatabase("Prizes.db"))
            {
                var people = db.GetCollection<Prize>("Prizes");
                List<GenericQuery<Prize>> qlist = new List<GenericQuery<Prize>>()
                {

                   new GenericQuery<Prize>(Query.All(), people)
                };
                List<UpdateValue<Prize>> ups = new List<UpdateValue<Prize>>();

                string column = "";
                string value = "";

                for (int i = 0; i < queryParams.Length; i++)
                {

                    string[] columnAndVal = queryParams[i].Split('=', '<', '>', '|');
                    column = columnAndVal[0];
                    value = columnAndVal[1];

                    if (queryParams[i].Contains("="))
                    {
                        GenericQuery<Prize> x = new GenericQuery<Prize>(Query.EQ(column, value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains("<"))
                    {
                        GenericQuery<Prize> x = new GenericQuery<Prize>(Query.StartsWith(column, value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains(">"))
                    {
                        GenericQuery<Prize> x = new GenericQuery<Prize>(Query.EQ(column.Skip(column.Length - value.Length).ToString(), value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains("|"))
                    {
                        GenericQuery<Prize> x = new GenericQuery<Prize>(Query.Contains(column, value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains("+"))
                    {
                        ups.Add(new UpdateValue<Prize>(column, value));
                    }
                }


                IEnumerable<Prize> per = qlist[0].Run();

                for (int i = 1; i < qlist.Count; i++)
                {
                    IEnumerable<Prize> peep = qlist[i].Run();
                    per = per.Where(x => peep.Contains(x));
                }

                if (ups.Count > 0)
                {
                    foreach (Prize p in per)
                    {
                        Prize q = p;
                        foreach (UpdateValue<Prize> up in ups)
                        {
                            q = up.Update(q);
                        }
                        DeletePrize(p);
                        AddPrize(q);
                    }
                }

                return per.ToArray();
            };
        }
        /// <summary>
        /// AddLocation() connect's to the Location database and add any location that is passed in through the parameter list
        /// </summary>
        /// <param name="p"></param>
        public static void AddLocation(Location p)
        {
            using (var db = new LiteDatabase("Locations.db"))
            {
                var people = db.GetCollection<Location>("Locations");
                // now we can carry out CRUD operations on the data

                people.Insert(Location.Clone(p));
            }
        }
        /// <summary>
        /// DeleteLocation() will connect to the Locations database and delete the specified 
        /// location, passed in through the params list.
        /// </summary>
        /// <param name="p"></param>
        public static void DeleteLocation(Location p)
        {
            using (var db = new LiteDatabase("Locations.db"))
            {
                var locations = db.GetCollection<Department>("Locations");
          //      locations.Delete(p);
            }
        }
        /// <summary>
        ///     In GetLocations(), passing in a string array of queries.
        ///     
        ///     We create a new list of type <GenericQuery<Locations>> named qlist that stores all
        ///     the queries that eventually gets populated and executed.
        ///     
        ///     Following, we then create a new list of type <GenericQuery<Locations>> named ups, in which stores all of the update
        ///     queries that also eventually gets populated and executed,
        ///     
        ///     Following, the column and value variables get declared ouside of the for loop so they dont get re-declared
        ///     with in every itteration of the for loop.
        ///     
        ///     In the for loop, we are itteratinng through every index in the queryParams[], depending on the query in the current index refrence,
        ///     we then add that query to qlist to add the execution at the end of the for loop.
        ///     
        ///     We then itterate through the qlist and execute updates and queries.
        ///     
        /// 
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns>Location[]</returns>
        public static Location[] GetLocations(string[] queryParams)
        {

            using (var db = new LiteDatabase("Locations.db"))
            {
                var people = db.GetCollection<Location>("Locations");
                List<GenericQuery<Location>> qlist = new List<GenericQuery<Location>>()
                {

                   new GenericQuery<Location>(Query.All(), people)
                };
                List<UpdateValue<Location>> ups = new List<UpdateValue<Location>>();

                string column = "";
                string value = "";

                for (int i = 0; i < queryParams.Length; i++)
                {

                    string[] columnAndVal = queryParams[i].Split('=', '<', '>', '|');
                    column = columnAndVal[0];
                    value = columnAndVal[1];

                    if (queryParams[i].Contains("="))
                    {
                        GenericQuery<Location> x = new GenericQuery<Location>(Query.EQ(column, value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains("<"))
                    {
                        GenericQuery<Location> x = new GenericQuery<Location>(Query.StartsWith(column, value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains(">"))
                    {
                        GenericQuery<Location> x = new GenericQuery<Location>(Query.EQ(column.Skip(column.Length - value.Length).ToString(), value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains("|"))
                    {
                        GenericQuery<Location> x = new GenericQuery<Location>(Query.Contains(column, value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains("+"))
                    {
                        ups.Add(new UpdateValue<Location>(column, value));
                    }
                }


                IEnumerable<Location> per = qlist[0].Run();

                for (int i = 1; i < qlist.Count; i++)
                {
                    IEnumerable<Location> peep = qlist[i].Run();
                    per = per.Where(x => peep.Contains(x));
                }

                if (ups.Count > 0)
                {
                    foreach (Location p in per)
                    {
                        Location q = p;
                        foreach (UpdateValue<Location> up in ups)
                        {
                            q = up.Update(q);
                        }
                        DeleteLocation(p);
                        AddLocation(q);
                    }
                }

                return per.ToArray();
            };
        }






        /// <summary>
        /// AddPerson() will connect to the People database and add a person based on the person passed
        /// in through the 
        /// </summary>
        /// <param name="p"></param>

        public static void AddPerson(Person p)
        {
            using (var db = new LiteDatabase("People.db"))
            {
                var people = db.GetCollection<Person>("People");
                // now we can carry out CRUD operations on the data

                people.Insert(Person.Clone(p));
            }

        }

    /// <summary>
    /// DeletePerson() connects to the people database and deletes a person based on the person passed in through the
    /// params list.
    /// </summary>
    /// <param name="p"></param>
        public static void DeletePerson(Person p)
        {
            using (var db = new LiteDatabase("People.db"))
            {
                var people = db.GetCollection<Department>("People");
          //      departments.Delete(p);
            }
        }




        /// <summary>
        ///     In GetPerson(), passing in a string array of queries.
        ///     
        ///     We create a new list of type <GenericQuery<Person>> named qlist that stores all
        ///     the queries that eventually gets populated and executed.
        ///     
        ///     Following, we then create a new list of type <GenericQuery<Person>> named ups, in which stores all of the update
        ///     queries that also eventually gets populated and executed,
        ///     
        ///     Following, the column and value variables get declared ouside of the for loop so they dont get re-declared
        ///     with in every itteration of the for loop.
        ///     
        ///     In the for loop, we are itteratinng through every index in the queryParams[], depending on the query in the current index refrence,
        ///     we then add that query to qlist to add the execution at the end of the for loop.
        ///     
        ///     We then itterate through the qlist and execute updates and queries.
        ///     
        /// 
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns>Person[]</returns>
        public static Person[] GetPersons(string[] queryParams)
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
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains(">"))
                    {
                        GenericQuery<Person> x = new GenericQuery<Person>(Query.EQ(column.Skip(column.Length - value.Length).ToString(), value), people);
                        qlist.Add(x);
                    }
                    else if (queryParams[i].Contains("|"))
                    {
                        GenericQuery<Person> x = new GenericQuery<Person>(Query.Contains(column, value), people);
                        qlist.Add(x);
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
}
