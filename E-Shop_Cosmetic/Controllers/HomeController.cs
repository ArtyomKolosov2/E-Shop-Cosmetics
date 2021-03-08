using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Specifications;
using E_Shop_Cosmetic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IProductsRepository _productsRepository;

        public HomeController(ILogger<HomeController> logger, IProductsRepository productsRepository)
        {
            _logger = logger;
            _productsRepository = productsRepository;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "E-Cosmetics";
            var messageBuilder = new StringBuilder($"Приветствуем на сайте");

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
                Products = await _productsRepository.GetAll
                (
                    new ProductSpecification()
                        .IncludeCategory()
                        .SortByPrice()
                        .WithoutTracking()
                        .AddPagination(9)
                ),
            };

            _logger.LogInformation("Home/Index is executed");

            return View(obj);
        }
    }
}
