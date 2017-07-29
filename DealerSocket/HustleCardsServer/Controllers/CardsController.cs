using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NWA.HustleCards.BackEnd;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HustleCardsServer
{
    [Route("api/card")]
    public class CardsController : Controller
    {
        // GET: api/person
        [HttpGet]
        public IActionResult Get()
        {
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, false);
            return Ok(DB_Operations.GetCards(qs));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post()
        {
            HustleCard p = default(HustleCard);
            string[] quearyParams = HttpContext.Request.Query["create"].ToString().Split('(', ')', ',');
            p = EasyQueary.ReflectionUpdate(p, quearyParams);
            DB_Operations.AddCard(p);
            return Ok("Added person!");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put()
        {
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, true);
            return Ok(DB_Operations.GetCards(qs));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete()
        {
            int count = 0;
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, false);
            HustleCard[] ps = DB_Operations.GetCards(qs);
            foreach (HustleCard p in ps)
            {
                DB_Operations.DeleteCard(p);
                count++;
            }
            return Ok($"deleted {count} cards");
        }
    }
}
