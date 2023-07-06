using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeERP.Model.Tickets;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeERP.Controllers.Departments
{
    public class EngineerController : Controller
    {
        [HttpGet]
        [Route("/engineer/index")]
        public IActionResult Index([FromCookie(Name = "user_id")] string? userId)
        {
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            // Query list of engineer ticket
            Engineer engineer = new Engineer();

            return View(engineer.Tickets());
        }
    }
}

