using Microsoft.AspNetCore.Mvc;
using FreeERP.Model;
using FreeERP.CustomModelBinder;

namespace FreeERP.Controllers
{
    public class Test : Controller
    {
        public IActionResult Index([Bind(nameof(TestModel.Name))] TestModel test)
        {
            if (!ModelState.IsValid)
            {
                List<string> errorsList = ModelState.Values
                    .SelectMany(value => value.Errors)
                    .Select(error => error.ErrorMessage)
                    .ToList();
                return BadRequest(string.Join("\n", errorsList));
            }
            return View();
        }

        public IActionResult Index2([FromBody][ModelBinder(BinderType = typeof(TestModelBinder))] TestModel test)
        {
            return Ok();
        }

        [Route("programming-language")]
        public IActionResult ProgrammingLanguages()
        {
            ListModel listModel = new ListModel();
            listModel.ListTitle = "";
            listModel.ListItems = new List<string>();

            return PartialView("_ListPartialView", listModel);
        }
    }
}
