using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeERP.Model.Tickets;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeERP.Controllers
{
    public class SaleTicketController : Controller
    {
        [HttpPost]
        [Route("/ticket/sale")]
        public IActionResult CreateSaleTicket(
            [FromForm(Name = "product")] string product,
            [FromForm(Name = "content")] string content)
        {
            string? userId = Request.Cookies["user_id"];
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            userId = "1";

            SaleTicket saleTicket = new SaleTicket(userId, content, product);
            string error = saleTicket.SaveToDB();
            if (error != "") {
                return Ok(error);
            }

            return View();
        }
    }
}
