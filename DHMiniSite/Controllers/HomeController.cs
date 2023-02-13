using DHData.Models;
using DHMiniSite.Operations.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DHMiniSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostOperations postOperations;
        public HomeController(IPostOperations postOperations)
        {
            this.postOperations = postOperations;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 0)
        {
            if (page > 0)
            {
                page--;
            }
            var list = await postOperations.GetAsync(page);
            return View(list);
        }

    }
}
