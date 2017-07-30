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
    /// The web API controller for interfacing with Departments in the database back-end
    /// </summary>
    [Route("api/department")]
    public class DepartmentController : Controller
    {
        /// <summary>
        /// Run a query on the Departments database, and return the collection of selected Departments.
        /// Does not allow write operations to be executed
        /// </summary>
        /// <returns>the collection of selected Departments which fulfill the provided query</returns>
        [HttpGet]
        public JsonResult Get()
        {
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, false);
            return Json(DB_Operations.GetDepartments(qs));
        }

        /// <summary>
        /// Parse out and construct a new Department using data passed in from the query string, then adds it to the Departments database.
        /// </summary>
        /// <returns>An okay message!</returns>
        [HttpPost]
        public IActionResult Post()
        {
            Department p = new Department();
            string[] quearyParams = HttpContext.Request.Query["create"].ToString().Split('(', ')', ',');
            p = EasyQueary.ReflectionUpdate(p, quearyParams);
            DB_Operations.AddDepartment(p);
            return Ok("Added Prize!");
        }

        /// <summary>
        /// Run a query on the Departments database, and return the collection of selected Departments.
        /// Does allow write operations to be executed mid-queary
        /// </summary>
        /// <returns>the collection of selected Departments which fulfill the provided query</returns>
        [HttpPut("{id}")]
        public IActionResult Put()
        {
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, true);
            return Ok(DB_Operations.GetDepartments(qs));
        }

        /// <summary>
        /// Run a query on the Departments database, and removes all records of the collection of selected Departments.
        /// </summary>
        /// <returns>An okay message with the number of records removed</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete()
        {
            int count = 0;
            string[] qs = EasyQueary.ConvertQueary(HttpContext.Request.Query, false);
            Department[] ps = DB_Operations.GetDepartments(qs);
            foreach (Department p in ps)
            {
                DB_Operations.DeleteDepartment(p);
                count++;
            }
            return Ok($"deleted {count} Departments");
        }
    }
}
