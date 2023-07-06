using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeERP.Model;

namespace MyFirstApp.Controllers
{
    public class HomeController : Controller
    {
        [Route("/home")]
        [Route("/")]
        public IActionResult Index()
        {
            string? userId = Request.Cookies["user_id"];
            if (userId != null)
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
