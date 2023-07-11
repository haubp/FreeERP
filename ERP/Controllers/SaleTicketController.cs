using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeERP.Model.Tickets;
using FreeERP.UIModel;

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

            SaleTicket saleTicket = new (userId, DateTime.Now, content, product);
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

            UISaleTicket uiSaleTicker = UIModelFactory.CreateUIModelSaleTicket(
                Convert.ToInt64(ticket_id), 
                Convert.ToInt64(ticket.UserID), 
                ticket.DateCreated, 
                ticket.Product, 
                ticket.Content);

            return View(uiSaleTicker);
        }

        [HttpPost]
        [Route("/ticket/sale/{ticket_id}")]
        public IActionResult UpdateTicket([FromRoute(Name = "ticket_id")] string ticket_id, [FromBody] string status)
        {
            string error = SaleTicketFactory.UpdateTicketStatusById(ticket_id, status);
            if (error != "")
            {
                return Ok(error);
            }

            return View();
        }
    }
}
