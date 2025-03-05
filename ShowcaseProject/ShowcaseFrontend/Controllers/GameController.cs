using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace ShowcaseFrontend.Controllers
{
    [Authorize]
    public class GameController : Controller
    {

        public IActionResult Index()
        {

            return View();
        }
    }
}
