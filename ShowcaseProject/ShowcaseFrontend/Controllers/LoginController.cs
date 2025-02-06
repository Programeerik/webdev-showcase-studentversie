using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Showcase_Contactpagina.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("NoSSL");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            var loginData = new { email, password };
            var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/login", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                HttpContext.Session.SetString("AuthToken", result);
                HttpContext.Session.SetString("UserEmail", email);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Login mislukt!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AuthToken");
            HttpContext.Session.Remove("UserEmail");

            return RedirectToAction("Index", "Home");
        }
    }
}
