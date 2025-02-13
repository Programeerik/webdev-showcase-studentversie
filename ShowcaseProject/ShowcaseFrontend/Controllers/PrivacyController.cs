using Microsoft.AspNetCore.Mvc;

namespace Showcase_Contactpagina.Controllers
{
    public class PrivacyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
