using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeaLib;
using TeaDB.Models;
using Microsoft.Extensions.Logging;

namespace TeaAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ManagerController : Controller
    {
        readonly ManagerService managerService;
        readonly ILogger<BasketController> logger;
        public ManagerController(ILogger<BasketController> logger)
        {
            managerService = new ManagerService();
            this.logger = logger;
        }

        [HttpGet("get/location/{id}")]
        [Produces("application/json")]
        public IActionResult GetLocationInventory(int id)
        {
            try
            {
                return Ok(managerService.GetLocationInventory(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpPut("put/location")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult ReplenishStock(InventoryModel inventory)
        {
            try
            {
                managerService.ReplenishStock(inventory);
                logger.LogInformation($"Stock replenished locationId: {inventory.locationId} productId: {inventory.productId}");
                return CreatedAtAction("ReplenishStock", inventory);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("get/locationorder/most/{id}")]
        [Produces("application/json")]
        public IActionResult GetOrderHistoryLocationByMostExpensive(int id)
        {
            try
            {
                return Ok(managerService.GetOrderHistoryLocationByMostExpensive(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("get/locationorder/least/{id}")]
        [Produces("application/json")]
        public IActionResult GetOrderHistoryLocationByLeastExpensive(int id)
        {
            try
            {
                return Ok(managerService.GetOrderHistoryLocationByLeastExpensive(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("get/locationorder/{id}")]
        [Produces("application/json")]
        public IActionResult GetLocationOrderHistory(int id)
        {
            try
            {
                return Ok(managerService.GetLocationOrderHistory(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost("add/product")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult CreateNewProduct(ProductModel product)
        {
            try
            {
                managerService.CreateNewProduct(product);
                return CreatedAtAction("CreateNewProduct", product);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [HttpPost("add/inventory")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult AddItemToInventory(InventoryModel inventory)
        {
            try
            {
                managerService.AddItemToInventory(inventory);
                return CreatedAtAction("AddItemToInventory", inventory);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("get/products")]
        [Produces("application/json")]
        public IActionResult GetAllProducts()
        {
            try
            {
                return Ok(managerService.GetAllProducts());
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
