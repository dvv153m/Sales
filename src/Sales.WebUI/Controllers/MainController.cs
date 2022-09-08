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

        public MainController(ILogger<MainController> logger)
        {            
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));     
        }

        public IActionResult Index()
        {            
            string promocode = "---";

            var claim = HttpContext.User.Claims.FirstOrDefault(p => p.Type == "Promocode");
            if (claim != null)
            {
                promocode = claim.Value;
            }

            return View(new MainViewModel { Promocode = promocode, Products = GetProducts() });
        }

        private IEnumerable<ProductViewModel> GetProducts()
        {
            return new List<ProductViewModel> 
            {
                new ProductViewModel
                { 
                    Id = 1,
                    Title = "book1",
                    Price = 500,
                    CopyNumber = 10,
                    ProductDetails = new List<ProductDetailViewModel>
                    {
                        new ProductDetailViewModel
                        {
                            Attribute = "Автор", Value = "Author1",
                        },
                        new ProductDetailViewModel
                        {
                            Attribute = "Год издания", Value = "2000",
                        },
                        new ProductDetailViewModel
                        {
                            Attribute = "ISBN код", Value = "2132423452",
                        }
                    }
                },
                new ProductViewModel
                {
                    Id = 2,
                    Title = "book2",
                    Price = 700,
                    CopyNumber = 4,
                    ProductDetails = new List<ProductDetailViewModel>
                    {
                        new ProductDetailViewModel
                        {
                            Attribute = "Автор", Value = "Author2",
                        },
                        new ProductDetailViewModel
                        {
                            Attribute = "Год издания", Value = "2002",
                        },
                        new ProductDetailViewModel
                        {
                            Attribute = "ISBN код", Value = "7878787878",
                        }
                    }
                }
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult check(int productId)
        {

            return RedirectToAction("Index");
        }
    }
}