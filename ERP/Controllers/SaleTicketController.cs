using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeERP.Model.Tickets;
using FreeERP.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeERP.Controllers
{
    public class SaleTicketController : Controller
    {
        [HttpPost]
        [Route("/ticket/sale")]
        public IActionResult CreateSaleTicket(
            [FromForm(Name = "product")] string product,
            [FromForm(Name = "content")] string content,
            [FromForm(Name = "priority")] string priority)
        {
            string? userId = Request.Cookies["user_id"];
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            SaleTicket saleTicket = new ("", userId, DateTime.Now, content, product, priority);
            string error = saleTicket.SaveToDB();
            if (error != "") {
                return Ok(error);
            }

            return View();
        }

        [HttpGet]
        [Route("/ticket/sale/{ticket_id}")]
        public IActionResult TicketDetail([FromRoute(Name = "ticket_id")] string ticket_id)
        {
            SaleTicket? ticket = SaleTicketFactory.QueryTicketById(ticket_id);
            if (ticket == null)
            {
                return BadRequest("Ticket id not found");
            }

            return View(ticket);
        }

        [HttpPost]
        [Route("/ticket/sale/{ticket_id}")]
        public IActionResult UpdateTicket([FromRoute(Name = "ticket_id")] string ticket_id, [FromBody] SaleTicketPostData ticketData)
        {
            string error = SaleTicketFactory.UpdateTicketStatusById(ticket_id, ticketData.Status!);
            if (error != "")
            {
                return Ok(error);
            }

            return Ok("Ticket update successfully");
        }
    }
}
