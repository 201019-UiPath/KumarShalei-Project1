using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
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

        [HttpGet("get/location/{id}")]
        [Produces("application/json")]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult GetLocationInventory(int id)
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


        [HttpGet("get/product/{id}")]
        [Produces("application/json")]
        
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                return Ok(locationService.GetProduct(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpPost("add/basket")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult CreateNewBasket(OrderModel order)
        {
            try
            {
                locationService.CreateNewBasket(order);
                return CreatedAtAction("CreateNewBasket", order);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet("get/order/{locationid}/{customerid}")]
        [Produces("application/json")]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult GetCurrentOrder(int customerId, int locationId)
        {
            try
            {
                return Ok(locationService.GetCurrentOrder(customerId, locationId));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpPost("add/basketitem")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult AddToBasket(OrderItemModel order)
        {
            try
            {
                locationService.AddToBasket(order);
                return CreatedAtAction("AddToBasket", order);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpPut("put/totalprice")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult IncreaseTotalPrice(OrderModel order, decimal amount)
        {
            try
            {
                locationService.IncreaseTotalPrice(order, amount);
                return CreatedAtAction("IncreaseTotalPrice", order, amount);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
       
    }
}
