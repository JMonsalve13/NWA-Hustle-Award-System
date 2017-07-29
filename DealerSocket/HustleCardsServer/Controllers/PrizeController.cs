using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NWA.HustleCards.BackEnd;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HustleCardsServer.Controllers
{
    [Route("api/prize")]
    public class PrizeController : Controller
    {
        // GET: api/person
        [HttpGet]
        public IActionResult Get()
        {
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, false);
            return Ok(DB_Operations.GetPrizes(qs));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post()
        {
            Prize p = default(Prize);
            string[] quearyParams = HttpContext.Request.Query["create"].ToString().Split('(', ')', ',');
            p = EasyQueary.ReflectionUpdate(p, quearyParams);
            DB_Operations.AddPrize(p);
            return Ok("Added Prize!");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put()
        {
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, true);
            return Ok(DB_Operations.GetPrizes(qs));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete()
        {
            int count = 0;
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, false);
            Prize[] ps = DB_Operations.GetPrizes(qs);
            foreach (Prize p in ps)
            {
                DB_Operations.DeletePrize(p);
                count++;
            }
            return Ok($"deleted {count} Prizes");
        }
    }
}
