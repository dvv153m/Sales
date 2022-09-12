using Microsoft.AspNetCore.Mvc;
using Sales.Contracts.Models;
using Sales.Core.Exceptions;
using Sales.Core.Interfaces.Services;
using Sales.WebUI.Models;
using System.Diagnostics;

namespace Sales.WebUI.Controllers
{
    public class MainController : Controller
    {
        private readonly IProductClient _productClient;
        private readonly IOrderClient _orderClient;
        private readonly ILogger<MainController> _logger;          

        public MainController(IProductClient productClient,   
                              IOrderClient orderClient,
                              ILogger<MainController> logger)
        {            
            _productClient = productClient ?? throw new ArgumentNullException(nameof(productClient));   
            _orderClient = orderClient ?? throw new ArgumentNullException(nameof(orderClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index()
        {            
            string promocode = GetPromocodeFromCookie() ?? "---";

            var products = await _productClient.GetAllAsync();
            var productsViewModel = Map(products);

            //todo запросить товары из корзины
            
            return View(new MainViewModel { Promocode = promocode, Products = productsViewModel, Cart = Enumerable.Empty<ProductViewModel>() });
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
            try
            {
                string? promocode = GetPromocodeFromCookie()+"1";
                if (promocode != null)
                {
                    await _orderClient.AddProductToOrder(promocode, productId, quantity: 1);
                }
            }
            catch (OrderException ex)
            {
                _logger.LogError($"товар не удалось добавить в корзину. Eception: {ex}");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteFromCart(int productId)
        {
            try
            {
                string? promocode = GetPromocodeFromCookie();
                if (promocode != null)
                {
                    await _orderClient.DeleteFromCart(promocode, productId);
                }
            }
            catch (OrderException ex)
            {
                _logger.LogError($"товар не удалось добавить в корзину. Eception: {ex}");
            }

            return RedirectToAction("Index");
        }

        public IActionResult CreateOrder()
        {
            return RedirectToAction("Index");
        }

        private string? GetPromocodeFromCookie()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(p => p.Type == "Promocode");
            if (claim != null)
            {
                return claim.Value;
            }
            else
            {
                return null;
            }
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