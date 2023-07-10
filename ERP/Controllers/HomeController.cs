using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeERP.Model;

namespace FreeERP.Controllers
{
    public class HomeController : Controller
    {
        [Route("/home")]
        [Route("/")]
        public IActionResult Index()
        {
            string? userId = Request.Cookies["user_id"];
            if (userId != null && userId != "")
            {
                Credential credential = new (userId);
                switch (credential.Type)
                {
                    case AccountType.Customer:
                        return RedirectToAction("Index", "Customer");
                    case AccountType.CustomerSuccess:
                        return RedirectToAction("Index", "CustomerSuccess");
                    case AccountType.Sale:
                        return RedirectToAction("Index", "Sale");
                    case AccountType.Engineer:
                        return RedirectToAction("Index", "Engineer");
                }
            }

            return View();
        }
    }
}
