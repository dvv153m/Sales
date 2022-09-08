using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sales.Contracts.Configuration;
using Sales.WebUI.Models;
using System.Diagnostics;

namespace Sales.WebUI.Controllers
{
    public class MainController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly WebUIOptions _config;
        private readonly ILogger<MainController> _logger;        

        public MainController(IHttpClientFactory httpClientFactory,
                              IOptions<WebUIOptions> config,
                              ILogger<MainController> logger)
        {            
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));           
            if (config?.Value == null) throw new ArgumentNullException(nameof(config));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _config = config.Value;
        }

        public IActionResult Index()
        {            
            string promocode = "---";

            var claim = HttpContext.User.Claims.FirstOrDefault(p => p.Type == "Promocode");
            if (claim != null)
            {
                promocode = claim.Value;
            }

            var products = GetProducts();
            var p = products.Take(2);

            return View(new MainViewModel { Promocode = promocode, Products = products, Cart = products.Take(2) });
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Положить товар в корзину
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IActionResult AddToCart(int productId)
        {
            //promocodeId productId
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsync($"{_config.OrderApiUrl}/promocode/register", null);
            var newPromocode = await response.Content.ReadAsStringAsync();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteFromCart(int productId)
        {
            return RedirectToAction("Index");
        }

        public IActionResult CreateOrder()
        {
            return RedirectToAction("Index");
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
                },
                new ProductViewModel
                {
                    Id = 3,
                    Title = "book3",
                    Price = 800,
                    CopyNumber = 5,
                    ProductDetails = new List<ProductDetailViewModel>
                    {
                        new ProductDetailViewModel
                        {
                            Attribute = "Автор", Value = "Author3",
                        },
                        new ProductDetailViewModel
                        {
                            Attribute = "Год издания", Value = "2014",
                        },
                        new ProductDetailViewModel
                        {
                            Attribute = "ISBN код", Value = "4545667565",
                        }
                    }
                }
            };
        }
    }
}