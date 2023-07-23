using Microsoft.AspNetCore.Mvc;
using FreeERP.Model;

namespace FreeERP.Controllers
{
    public class DashboardController : Controller
    {
        [HttpGet]
        [Route("/dashboard/index")]
        public IActionResult Index()
        {
            DashBoardData data = new (
                Dashboard.QueryTotalTickets(),
                Dashboard.QueryTotalBills(),
                Dashboard.QueryTotalCustomers()
            );

            return View(data);
        }
    }
}
