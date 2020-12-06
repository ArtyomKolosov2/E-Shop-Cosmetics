using E_Shop_Cosmetic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;

namespace E_Shop_Cosmetic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            StringBuilder messageBuilder = new StringBuilder($"Приветствуем на сайте");
            ViewBag.Title = "Добро пожаловать";
            if (User.Identity.IsAuthenticated)
            {
                messageBuilder.Append($", {User.Identity.Name}!");
            }
            else
            {
                messageBuilder.Append('!');
            }
            var obj = new HomeViewModel
            {
                Message = messageBuilder.ToString(),
            };
            _logger.LogInformation("Home/Index is executed");
            return View(obj);
        }
    }
}
