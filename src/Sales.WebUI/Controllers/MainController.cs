using Microsoft.AspNetCore.Mvc;
using Sales.Contracts.Models;
using Sales.Core.Interfaces.Services;
using Sales.WebUI.Models;
using System.Diagnostics;

namespace Sales.WebUI.Controllers
{
    public class MainController : Controller
    {
        private readonly IProductClient _productClient;                
        private readonly ILogger<MainController> _logger;          

        public MainController(IProductClient productClient,                                                            
                              ILogger<MainController> logger)
        {            
            _productClient = productClient ?? throw new ArgumentNullException(nameof(productClient));            
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index()
        {            
            string promocode = "---";

            var claim = HttpContext.User.Claims.FirstOrDefault(p => p.Type == "Promocode");
            if (claim != null)
            {
                promocode = claim.Value;
            }

            var products = await _productClient.GetAllAsync();
            var productsViewModel = Map(products);
            
            return View(new MainViewModel { Promocode = promocode, Products = productsViewModel, Cart = productsViewModel.Take(1) });
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
        public async Task<IActionResult> AddToCart(int productId)
        {
            //promocodeId productId
            //todo это делать через сервис OrderClient
            /*var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsync($"{_config.OrderApiUrl}/promocode/register", null);
            var newPromocode = await response.Content.ReadAsStringAsync();*/

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


        private IEnumerable<ProductViewModel> Map(IEnumerable<ProductDto> products)
        {
            var productDtos = new List<ProductViewModel>();
            foreach (ProductDto product in products)
            {
                productDtos.Add(Map(product));
            }
            return productDtos;
        }

        private ProductViewModel Map(ProductDto product)
        {
            var productDetails = new List<ProductDetailViewModel>();
            foreach (var productDetail in product.ProductDetails)
            {
                productDetails.Add(new ProductDetailViewModel
                {
                    Attribute = productDetail.Attribute,
                    Value = productDetail.Value
                });
            }
            return new ProductViewModel
            {
                Id = product.Id,
                Title = product.Title,
                CopyNumber = product.CopyNumber,
                Price = product.Price,
                ProductDetails = productDetails
            };
        }
    }
}