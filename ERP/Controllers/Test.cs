using Microsoft.AspNetCore.Mvc;

namespace FreeERP.Controllers
{
    public class Test : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
