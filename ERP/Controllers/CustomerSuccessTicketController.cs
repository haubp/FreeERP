﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeERP.Model.Tickets;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeERP.Controllers
{
    public class CustomerSuccessTicketController : Controller
    {
        [HttpPost]
        [Route("/ticket/cs")]
        public IActionResult CreateCSTicket([FromForm(Name = "content")] string content)
        {
            string? userId = Request.Cookies["user_id"];
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            CustomerSuccessTicket ticket = new CustomerSuccessTicket(userId, content);
            string error = ticket.SaveToDB();
            if (error != "") {
                return Ok(error);
            }

            return View();
        }
    }
}

