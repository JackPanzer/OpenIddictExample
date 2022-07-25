using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace OpenIddictExample.SP.Application.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
