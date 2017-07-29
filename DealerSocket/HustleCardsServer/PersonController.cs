using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
            string fname = HttpContext.Request.Query["fname"].ToString();
            string lname = HttpContext.Request.Query["lname"].ToString();
            return Ok(new string[] { "Hi there", "How goes it?", fname, lname });
        }

        // GET api/person/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(new string[] { "Hola", "Como estas?" });
        }

        // GET api/person/dan,theMan
        [HttpGet("{fname, lname}")]
        public IActionResult Get(string fname, string lname)
        {
            return Ok(new string[] { "Hallo", "Wie geht's Ihnen?", fname, lname });
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
