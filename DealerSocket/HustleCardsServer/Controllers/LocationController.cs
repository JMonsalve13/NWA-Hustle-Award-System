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
    /// The web API controller for interfacing with Locations in the database back-end
    /// </summary>
    [Route("api/location")]
    public class LocationController : Controller
    {
        /// <summary>
        /// Run a query on the Locations database, and return the collection of selected Locations.
        /// Does not allow write operations to be executed
        /// </summary>
        /// <returns>the collection of selected Locations which fulfill the provided query</returns>
        [HttpGet]
        public IActionResult Get()
        {
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, false);
            return Ok(DB_Operations.GetLocations(qs));
        }

        /// <summary>
        /// Parse out and construct a new Location using data passed in from the query string, then adds it to the Locations database.
        /// </summary>
        /// <returns>An okay message!</returns>
        [HttpPost]
        public IActionResult Post()
        {
            Location p = default(Location);
            string[] quearyParams = HttpContext.Request.Query["create"].ToString().Split('(', ')', ',');
            p = EasyQueary.ReflectionUpdate(p, quearyParams);
            DB_Operations.AddLocation(p);
            return Ok("Added person!");
        }

        /// <summary>
        /// Run a query on the Locations database, and return the collection of selected Locations.
        /// Does allow write operations to be executed mid-queary
        /// </summary>
        /// <returns>the collection of selected Locations which fulfill the provided query</returns>
        [HttpPut("{id}")]
        public IActionResult Put()
        {
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, true);
            return Ok(DB_Operations.GetLocations(qs));
        }

        /// <summary>
        /// Run a query on the Locations database, and removes all records of the collection of selected Locations.
        /// </summary>
        /// <returns>An okay message with the number of records removed</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete()
        {
            int count = 0;
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, false);
            Location[] ps = DB_Operations.GetLocations(qs);
            foreach (Location p in ps)
            {
                DB_Operations.DeleteLocation(p);
                count++;
            }
            return Ok($"deleted {count} locations");
        }
    }
}
