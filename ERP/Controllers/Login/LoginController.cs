using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeERP.Controllers.Login
{
	public class LoginController : Controller
	{
		[HttpPost]
		[Route("/login")]
		public IActionResult Login([FromForm(name = "username")] string? username, [FromForm(name = "password")] string? password)
		{
			// Query user id
			var user = new
			{
				user_id = "",
				type = AccountType.Customer
			};

			// Set Cookie contains user id
			var cookieOptions = new CookieOptions
			{
				Name = "user_id",
				Value = "1",
				Expires = DateTime.Now.AddDays(1),
				Path = "/"
			};
			Response.Cookies.Append(cookieOptions.Name, cookieOptions.Value, cookieOptions);

			switch (user.type)
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

			return OK(user);
		}
	}

	public enum AccountType
	{
		Customer,
		CustomerSuccess,
		Sale,
		Engineer
	}
}
