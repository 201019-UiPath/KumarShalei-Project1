﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using TeaDB.Models;
using TeaLib;

namespace TeaAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    
    public class MainMenuController : Controller
    {
        private readonly MainMenuService mainMenuService;

        public MainMenuController()
        {
            this.mainMenuService = new MainMenuService(); 
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

        [HttpGet("get/order/least/{email}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult GetCustomerOrderLeastToMost(string email)
        {
            try
            {
                return Ok(mainMenuService.GetCustomerOrderLeastToMost(email));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpGet("get/order/most/{email}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult GetCustomerOrderMostToLeast(string email)
        {
            try
            {
                return Ok(mainMenuService.GetCustomerOrderMostToLeast(email));
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
                return CreatedAtAction("NewCustomer",customer);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
