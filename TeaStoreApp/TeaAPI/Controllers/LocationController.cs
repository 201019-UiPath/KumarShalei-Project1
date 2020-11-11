using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeaDB;
using TeaDB.Models;
using TeaLib;


namespace TeaAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LocationController : Controller
    {
        private readonly LocationService locationService;

        public LocationController()
        {
            this.locationService = new LocationService();
        }

        [HttpGet("get/{id}")]
        [Produces("application/json")]
        public IActionResult GetCustomerOrders(int id)
        {
            try
            {
                return Ok(locationService.GetLocationInventory(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
