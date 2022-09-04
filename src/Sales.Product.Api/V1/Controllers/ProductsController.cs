using Microsoft.AspNetCore.Mvc;
using Sales.Contracts.Models;
using Sales.Contracts.Request.Product;
using Sales.Core.Interfaces.Services;

namespace Sales.Product.Api.V1.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService,
                                 ILogger<ProductsController> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {            
            try
            {
                var products = await _productService.GetAll();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get products");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var products = await _productService.GetById(id);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get products");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            try
            {
                ProductDto productDto = await _productService.AddAsync(request);
                return CreatedAtRoute(routeName: String.Empty,
                                      routeValues: new { id = productDto.Id },
                                      value: productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add product");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }
    }
}
