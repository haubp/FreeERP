using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeERP.Model.Tickets;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeERP.Controllers.tickets
{
    public class EngineerTicketController : Controller
    {
        [HttpPost]
        [Route("/ticket/engineer")]
        public IActionResult CreateEngineerTicket([FromForm(Name = "content")] string content)
        {
            string? userId = Request.Cookies["user_id"];
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            EngineerTicket ticket = new EngineerTicket(userId, content);
            ticket.SaveToDB();

            return View();
        }
    }
}

