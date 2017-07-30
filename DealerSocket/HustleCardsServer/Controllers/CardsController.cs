using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NWA.HustleCards.BackEnd;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HustleCardsServer
{
    /// <summary>
    /// The web API controller for interfacing with cards in the database back-end
    /// </summary>
    [Route("api/card")]
    public class CardsController : Controller
    {
        
        /// <summary>
        /// Run a query on the HustleCards database, and return the collection of selected cards.
        /// Does not allow write operations to be executed
        /// </summary>
        /// <returns>the collection of selected cards which fulfill the provided query</returns>
        [HttpGet]
        public JsonResult Get()
        {
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, false);
            return Json(DB_Operations.GetCards(qs));
        }

        /// <summary>
        /// Parse out and construct a new card using data passed in from the query string, then adds it to the cards database.
        /// </summary>
        /// <returns>An okay message!</returns>
        [HttpPost]
        public IActionResult Post()
        {
            HustleCard p = new HustleCard();
            string[] quearyParams = HttpContext.Request.Query["create"].ToString().Split('(', ')', ',');
            p = EasyQueary.ReflectionUpdate(p, quearyParams);
            DB_Operations.AddCard(p);
            return Ok("Added person!");
        }

        /// <summary>
        /// Run a query on the HustleCards database, and return the collection of selected cards.
        /// Does allow write operations to be executed mid-queary
        /// </summary>
        /// <returns>the collection of selected cards which fulfill the provided query</returns>
        [HttpPut("{id}")]
        public JsonResult Put()
        {
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, true);
            return Json(DB_Operations.GetCards(qs));
        }

        /// <summary>
        /// Run a query on the HustleCards database, and removes all records of the collection of selected cards.
        /// </summary>
        /// <returns>An okay message with the number of records removed</returns>
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
