using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeERP.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeERP.Controllers.Departments
{
    public class SaleController : Controller
    {
        [HttpGet]
        [Route("/sale/index")]
        public IActionResult Index()
        {
            string? userId = Request.Cookies["user_id"];
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

