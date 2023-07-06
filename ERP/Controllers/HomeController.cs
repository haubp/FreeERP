using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFirstApp.Models;

namespace MyFirstApp.Controllers
{
    public class HomeController : Controller
    {
        [Route("/home")]
        [Route("/")]
        public IActionResult Index([FromCookie(Name = "user_id")] string? userId)
        {
            if (userId)
            {
                Credential credential = new Credential(userId);
                switch (credential.type)
                {
                    case AccountType.Customer:
                        return RedirectToAction("Customer", "Index");
                    case AccountType.CustomerSuccess:
                        return RedirectToAction("CustomerSuccess", "Index");
                    case AccountType.Sale:
                        return RedirectToAction("Sale", "Index");
                    case AccountType.Engineer:
                        return RedirectToAction("Engineer", "Index");
                }
            }

            return View();
        }
    }
}
