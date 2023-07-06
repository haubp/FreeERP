using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeERP.Model.Tickets;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeERP.Controllers.Departments
{
    public class CustomerSuccessController : Controller
    {
        [HttpGet]
        [Route("/cs/index")]
        public IActionResult Index([FromCookie(Name = "user_id")] string? userId)
        {
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            // Query list of customer success ticket
            CustomerSuccess cs = new CustomerSuccess();

            return View(cs.Tickets());
        }
    }
}

