using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TeaWeb.Controllers
{
    [Route("user")]
    public class SignInController : Controller
    {
        const string url = "https://localhost:5001/api/";
        public SignInController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
