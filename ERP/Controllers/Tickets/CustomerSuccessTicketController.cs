﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeERP.Model.Tickets;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeERP.Controllers.tickets
{
    public class CustomerSuccessTicketController : Controller
    {
        [HttpPost]
        [Route("/ticket/cs")]
        public IActionResult CreateCSTicket(
            [FromCookie(Name = "user_id")] string? userId,
            [FromForm(Name = "content")] string? content)
        {
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            CustomerSuccessTicket ticket = new CustomerSuccessTicket(userId, content);
            ticket.SaveToDB();

            return View();
        }
    }
}

