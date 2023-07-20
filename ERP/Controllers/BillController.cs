using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FreeERP.Model;

namespace FreeERP.Controllers
{
    public class BillController : Controller
    {
        [HttpPost]
        [Route("/bill")]
        public IActionResult CreateBill(
            [FromForm(Name = "UserID")] string userId,
            [FromForm(Name = "Content")] string content,
            [FromForm(Name = "Amount")] int amount)
        {
            Bill bill = new(userId, content, amount);
            bill.SaveToDB();
            return View();
        }

        [HttpGet]
        [Route("/bill/{bill_id}")]
        public IActionResult BillDetail([FromRoute(Name = "bill_id")] string billId) {
            return View(BillFactory.QueryBillById(billId));
        }

        [HttpGet]
        [Route("/bill/qr/{bill_id}")]
        public IActionResult BillQR([FromRoute(Name = "bill_id")] string billId) {
            return View((object)billId);
        }

        [HttpPost]
        [Route("/bill/checkout/{bill_id}")]
        public IActionResult BillCheckout([FromRoute(Name = "bill_id")] string billId)
        {
            string error = BillFactory.Pay(billId);
            if (error != "")
            {
                return BadRequest(error);
            }
            return Ok("Successfully");
        }
    }
}

