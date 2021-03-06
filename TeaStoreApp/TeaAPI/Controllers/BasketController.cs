﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeaLib;
using TeaDB.Models;
using Serilog;
using Microsoft.Extensions.Logging;

namespace TeaAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BasketController : Controller
    {
        private readonly ILogger<BasketController> logger;
        readonly BasketService basketService;
        public BasketController(ILogger<BasketController> logger)
        {
            basketService = new BasketService();
            this.logger = logger;
        }


        [HttpGet("get/product/{id}")]
        [Produces("application/json")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                return Ok(basketService.GetProduct(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpDelete("delete/basketitem/")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult DeleteFromBasket(OrderItemModel order)
        {
            try
            {
                basketService.DeleteFromBasket(order);
                return CreatedAtAction("DeleteFromBasket", order);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpDelete("delete/basket/")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult DeleteBasket(OrderModel order)
        {
            try
            {
                basketService.DeleteBasket(order);
                return CreatedAtAction("DeleteBasket", order);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpPut("put/basketprice/")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult DecreaseTotalPrice(OrderModel order, decimal amount)
        {
            try
            {
                basketService.DecreaseTotalPrice(order, amount);
                return CreatedAtAction("DecreaseTotalPrice", order);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpPut("put/basketorder/")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult PlaceOrder(OrderModel order)
        {
            try
            {
                basketService.PlaceOrder(order);
                logger.LogInformation($"Order Placed at locationId: {order.locationId}");
                return CreatedAtAction("PlaceOrder", order);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpPut("put/stock")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult DecreaseStock(InventoryModel inventory)
        {
            try
            {
                basketService.DecreaseStock(inventory);
                return CreatedAtAction("DecreaseStock", inventory);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        
    }
}
