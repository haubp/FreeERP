﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeERP.Model.Tickets;
using FreeERP.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeERP.Controllers
{
    public class CustomerSuccessTicketController : Controller
    {
        [HttpPost]
        [Route("/ticket/cs")]
        public IActionResult CreateCSTicket([FromForm(Name = "content")] string content,
            [FromForm(Name = "priority")] string priority)
        {
            string? userId = Request.Cookies["user_id"];
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            CustomerSuccessTicket ticket = new CustomerSuccessTicket("", userId, DateTime.Now, content, priority);
            string error = ticket.SaveToDB();
            if (error != "") {
                return Ok(error);
            }

            return View();
        }

        [HttpGet]
        [Route("/ticket/cs/{ticket_id}")]
        public IActionResult TicketDetail([FromRoute(Name = "ticket_id")] string ticket_id)
        {
            CustomerSuccessTicket? ticket = CustomerSuccessTicketFactory.QueryTicketById(ticket_id);
            if (ticket == null)
            {
                return BadRequest("Ticket id not found");
            }

            return View(ticket);
        }

        [HttpPost]
        [Route("/ticket/cs/{ticket_id}")]
        public IActionResult UpdateTicket([FromRoute(Name = "ticket_id")] string ticket_id, [FromBody] CustomerSuccessTicketPostData ticketData)
        {
            string error = CustomerSuccessTicketFactory.UpdateTicketStatusById(ticket_id, ticketData.Status!);
            if (error != "")
            {
                return Ok(error);
            }

            return Ok("Ticket update successfully");
        }
    }
}

