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
    [Route("api/person")]
    public class PersonController : Controller
    {
        // GET: api/person
        [HttpGet]
        public IActionResult Get()
        {
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, false);
            return Ok(DB_Operations.GetPersons(qs));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post()
        {
            Person p = default(Person);
            string[] quearyParams = HttpContext.Request.Query["create"].ToString().Split('(',')',',');
            p = EasyQueary.ReflectionUpdate(p, quearyParams);
            DB_Operations.AddPerson(p);
            return Ok("Added person!");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put()
        {
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, true);
            return Ok(DB_Operations.GetPersons(qs));
        }

        // DELETE api/values/5
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
