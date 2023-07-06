using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeERP.Model.Tickets;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeERP.Controllers.tickets
{
    public class SaleTicketController : Controller
    {
        [HttpPost]
        [Route("/ticket/sale")]
        public IActionResult CreateSaleTicket(
            [FromCookie(Name = "user_id")] string? userId,
            [FromForm(Name = "products")] List<string>? products,
            [FromForm(Name = "content")] string? content)
        {
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            SaleTicket saleTicket = new SaleTicket(userId, content);
            saleTicket.SaveToDB();

            return View();
        }
    }
}
