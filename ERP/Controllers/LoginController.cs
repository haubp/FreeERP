using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeERP.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeERP.Controllers.Login
{
    public class LoginController : Controller
    {
        [Route("/register")]
        public IActionResult Register(
            [FromForm(Name = "username")] string username,
            [FromForm(Name = "password")] string password,
            [FromForm(Name = "type")] string type)
        {
            // Query user id
            Credential credential = new(username, password, type);
            string dbError = credential.CreateUser();

            return View(dbError);
        }

        [Route("/login")]
        public IActionResult Login(
            [FromForm(Name = "username")] string username,
            [FromForm(Name = "password")] string password)
        {
            // Query user id
            Credential credential = new (username, password, "");
            string user_id = credential.QueryUserIdFromUserNameAndPassword();

            // Set Cookie contains user id
            string cookieName = "user_id";
            string cookieValue = user_id;
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                Path = "/"
            };
            Response.Cookies.Append(cookieName, cookieValue, cookieOptions);

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

            return View();
        }

        [Route("/logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("user_id");

            return RedirectToAction("Index", "Home"); ;
        }
    }
}
