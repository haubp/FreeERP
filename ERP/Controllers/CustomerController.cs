using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeERP.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeERP.Controllers
{
    public class CustomerController : Controller
    {
        [HttpGet]
        [Route("/customer/index")]
        public IActionResult Index()
        {
            string? userId = Request.Cookies["user_id"];
            if (userId == null)
            {
                return RedirectToAction("Login", "Login");
            }

            // Query list of ticket created by Customer
            Customer customer = new (userId);

            return View(customer);
        }

        [HttpPost]
        [Route("/customer/tier")]
        public IActionResult UpgradeTier([FromForm(Name = "customerId")] string customerId, [FromForm(Name = "tier")] string tier) {
            string error = Customer.UpdateCustomerTier(customerId, tier);
            if (error != "") {
                return BadRequest(error);
            }
            return View();
        }
    }
}
