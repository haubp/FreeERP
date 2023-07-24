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
    public class EngineerTicketController : Controller
    {
        [HttpPost]
        [Route("/ticket/engineer")]
        public IActionResult CreateEngineerTicket([FromForm(Name = "content")] string content,
            [FromForm(Name = "priority")] string priority)
        {
            string? userId = Request.Cookies["user_id"];
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            EngineerTicket ticket = new EngineerTicket("", userId, DateTime.Now, content, priority);
            string error = ticket.SaveToDB();

            if (error != "") {
                return Ok(error);
            }

            return View();
        }

        [HttpGet]
        [Route("/ticket/engineer/{ticket_id}")]
        public IActionResult TicketDetail([FromRoute(Name = "ticket_id")] string ticket_id)
        {
            EngineerTicket? ticket = EngineerTicketFactory.QueryTicketById(ticket_id);
            if (ticket == null)
            {
                return BadRequest("Ticket id not found");
            }
            EngineerTicket uiEngineerTicker = EngineerTicketFactory.CreateUIModelEngineerTicket(
                Convert.ToInt64(ticket_id),
                Convert.ToInt64(ticket.UserID),
                ticket.DateCreated,
                ticket.Content,
                ticket.Status);

            return View(uiEngineerTicker);
        }

        [HttpPost]
        [Route("/ticket/engineer/{ticket_id}")]
        public IActionResult UpdateTicket([FromRoute(Name = "ticket_id")] string ticket_id, [FromBody] EngineerTicketPostData ticketData)
        {
            string error = EngineerTicketFactory.UpdateTicketStatusById(ticket_id, ticketData.status!);
            if (error != "")
            {
                return Ok(error);
            }

            return Ok("Ticket update successfully");
        }
    }
}

