using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeERP.Controllers.Departments
{
    public class SaleController : Controller
    {
        [HttpGet]
        [Route("/sale/index")]
        public IActionResult Index([FromCookie(Name = "user_id")] string? userId)
        {
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            // Query list of sale ticket
            Sale sale = new Sale();

            return View(sale.Tickets());
        }
    }
}

