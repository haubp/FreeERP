using Microsoft.AspNetCore.Mvc;
using FreeERP.Model.Tickets;

namespace FreeERP.Controllers
{
    public class CommentController : Controller
    {
        [HttpPost]
        [Route("/comment/{comment_type}/{ticket_id}")]
        public IActionResult Create([FromBody] CommentPostData commentData)
        {
            string result = CommentFactory.CreateComment(commentData.TicketID!, commentData.UserID!, commentData.Content!);
            if (result != "")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("/comment/{ticket_id}")]
        public IActionResult Get([FromRoute(Name = "ticket_id")] string ticket_id)
        {
            List<string> comments = CommentFactory.QueryCommentByTicketId(ticket_id);
            return Json(comments);
        }
    }
}
