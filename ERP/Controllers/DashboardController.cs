using Microsoft.AspNetCore.Mvc;

namespace FreeERP.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
