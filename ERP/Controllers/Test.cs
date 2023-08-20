using Microsoft.AspNetCore.Mvc;
using FreeERP.Model;
using FreeERP.CustomModelBinder;
using Services;
using ServiceContracts;

namespace FreeERP.Controllers
{
    public class TestController : Controller
    {
        private readonly ICitiesService _citiesService;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        // Constructor injection
        public TestController(ICitiesService service, IServiceScopeFactory serviceScopeFactory)
        {
            _citiesService = service;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [Route("/index1")]
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

        [Route("/index2")]
        public IActionResult Index2([FromBody][ModelBinder(BinderType = typeof(TestModelBinder))] TestModel test)
        {
            return Ok();
        }

        // Method injection
        [Route("/index3")]
        public IActionResult Index3([FromServices] ICitiesService citiesService)
        {
            return Ok();
        }

        // Method injection
        [Route("/index4")]
        public IActionResult Index4()
        {
            // Create new scope
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                // Inject service
                ICitiesService citiesService = scope.ServiceProvider.GetRequiredService<ICitiesService>();
            } // end of scope, it call service Dispose method

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

        [Route("view-component-dynamic")]
        public IActionResult GridViewComponent()
        {
            TestModel testModel = new ();

            return ViewComponent("Grid", new { x = 1, y = 2 });
        }
    }
}
