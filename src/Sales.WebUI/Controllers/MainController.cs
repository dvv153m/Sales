using Microsoft.AspNetCore.Mvc;
using Sales.Contracts.Models;
using Sales.Core.Rules.Products;
using Sales.WebUI.Models;
using System.Diagnostics;

namespace Sales.WebUI.Controllers
{
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;
        private readonly CartAddProductRules _cartRuleHandler;

        public MainController(CartAddProductRules cartRuleHandler,
                              ILogger<MainController> logger)
        {
            _cartRuleHandler = cartRuleHandler;
            _logger = logger;            
        }

        public IActionResult Index()
        {
            //здесь пока для теста проверка правил добвления в корзину

            string errorInfo = "";
            _cartRuleHandler.Handle(new Cart 
            {
                Products = new List<ProductDto>
                { 
                    new ProductDto{Id = 1, Title="book1", CopyNumber=1 },
                    new ProductDto{Id = 2, Title="book2", CopyNumber=0 }
                }
            }, new ProductDto { Id = 2, Title = "book2", CopyNumber = 0 });


            string promocode = "---";

            var claim = HttpContext.User.Claims.FirstOrDefault(p => p.Type == "Promocode");
            if (claim != null)
            {
                promocode = claim.Value;
            }

            return View(new UserViewModel { Promocode = promocode });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}