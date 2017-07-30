using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NWA.HustleCards.BackEnd;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HustleCardsServer
{
    /// <summary>
    /// The web API controller for interfacing with People in the database back-end
    /// </summary>
    [Route("api/person")]
    public class PersonController : Controller
    {
        /// <summary>
        /// Run a query on the People database, and return the collection of selected People.
        /// Does not allow write operations to be executed
        /// </summary>
        /// <returns>the collection of selected People which fulfill the provided query</returns>
        [HttpGet]
        public IActionResult Get()
        {
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, false);
            return Ok(DB_Operations.GetPersons(qs));
        }

        /// <summary>
        /// Parse out and construct a new Person using data passed in from the query string, then adds it to the People database.
        /// </summary>
        /// <returns>An okay message!</returns>
        [HttpPost]
        public IActionResult Post()
        {
            Person p = new Person();
            string[] quearyParams = HttpContext.Request.Query["create"].ToString().Split('(',')',',');
            p = EasyQueary.ReflectionUpdate(p, quearyParams);
            DB_Operations.AddPerson(p);
            return Ok("Added person!");
        }

        /// <summary>
        /// Run a query on the People database, and return the collection of selected People.
        /// Does allow write operations to be executed mid-queary
        /// </summary>
        /// <returns>the collection of selected People which fulfill the provided query</returns>
        [HttpPut("{id}")]
        public IActionResult Put()
        {
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, true);
            return Ok(DB_Operations.GetPersons(qs));
        }

        /// <summary>
        /// Run a query on the People database, and removes all records of the collection of selected People.
        /// </summary>
        /// <returns>An okay message with the number of records removed</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete()
        {
            int count = 0;
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, false);
            Person[] ps = DB_Operations.GetPersons(qs);
            foreach(Person p in ps)
            {
                DB_Operations.DeletePerson(p);
                count++;
            }
            return Ok($"deleted {count} people");
        }
    }
}
