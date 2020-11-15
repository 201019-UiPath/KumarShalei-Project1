using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using TeaDB.Models;
using TeaLib;
using Microsoft.Extensions.Logging;

namespace TeaAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    
    public class MainMenuController : Controller
    {
        private readonly ILogger<MainMenuController> logger;
        private readonly MainMenuService mainMenuService;

        public MainMenuController(ILogger<MainMenuController> logger)
        {
            this.mainMenuService = new MainMenuService();
            this.logger = logger;
        }

        [HttpGet("get/{email}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult GetCustomerInfo(string email)
        {
            try
            {
                return Ok(mainMenuService.GetCustomerInfo(email));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("get/order/least/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult GetCustomerOrderLeastToMost( int id)
        {
            try
            {
                return Ok(mainMenuService.GetCustomerOrderLeastToMost(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpGet("get/orders/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult GetCustomerOrders(int id)
        {
            try
            {
                return Ok(mainMenuService.GetCustomerOrders(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }



        [HttpGet("get/order/most/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult GetCustomerOrderMostToLeast(int id)
        {
            try
            {
                return Ok(mainMenuService.GetCustomerOrderMostToLeast(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpPost("add")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult NewCustomer(CustomerModel customer)
        {
            try
            {
                mainMenuService.NewCustomer(customer);
                logger.LogInformation("New Customer Added");
                return CreatedAtAction("NewCustomer", customer);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
