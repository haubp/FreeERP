using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreeERP.Controllers
{
    public class DemoController : Controller
    {
        [Route("/demo/index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/demo")]
        public IActionResult DownloadDemo()
        {
            string demoFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "demo/demo.png");

            // Return the file as a download response
            return PhysicalFile(demoFilePath, "application/octet-stream", "demo.png");
        }
    }
}

