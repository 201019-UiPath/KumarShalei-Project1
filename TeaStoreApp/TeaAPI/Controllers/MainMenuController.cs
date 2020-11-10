using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeaDB;
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
    }
}
