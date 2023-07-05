using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeERP.Controllers.tickets
{
    public class SaleTicketController : Controller
    {
        [HttpPut]
        [Route("/ticket/sale")]
        public IActionResult CreateSaleTicket([FromCookie(Name = "user_id")] string? userId)
        {
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }
    }
}

