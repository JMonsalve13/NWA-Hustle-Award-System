using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NWA.HustleCards.BackEnd;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HustleCardsServer.Controllers
{
    /// <summary>
    /// The web API controller for interfacing with Prizes in the database back-end
    /// </summary>
    [Route("api/prize")]
    public class PrizeController : Controller
    {
        /// <summary>
        /// Run a query on the Prizes database, and return the collection of selected Prizes.
        /// Does not allow write operations to be executed
        /// </summary>
        /// <returns>the collection of selected Prizes which fulfill the provided query</returns>
        [HttpGet]
        public JsonResult Get()
        {
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, false);
            return Json(DB_Operations.GetPrizes(qs));
        }

        /// <summary>
        /// Parse out and construct a new Prize using data passed in from the query string, then adds it to the Prizes database.
        /// </summary>
        /// <returns>An okay message!</returns>
        [HttpPost]
        public IActionResult Post()
        {
            Prize p = new Prize();
            string[] quearyParams = HttpContext.Request.Query["create"].ToString().Split('(', ')', ',');
            p = EasyQueary.ReflectionUpdate(p, quearyParams);
            DB_Operations.AddPrize(p);
            return Ok("Added Prize!");
        }

        /// <summary>
        /// Run a query on the Prizes database, and return the collection of selected Prizes.
        /// Does allow write operations to be executed mid-queary
        /// </summary>
        /// <returns>the collection of selected Prizes which fulfill the provided query</returns>
        [HttpPut("{id}")]
        public JsonResult Put()
        {
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, true);
            return Json(DB_Operations.GetPrizes(qs));
        }

        /// <summary>
        /// Run a query on the Prizes database, and removes all records of the collection of selected Prizes.
        /// </summary>
        /// <returns>An okay message with the number of records removed</returns>
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
