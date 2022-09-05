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

        [HttpGet("{id}", Name = nameof(GetProductById))]
        public async Task<IActionResult> GetProductById(long id)
        {
            try
            {
                var products = await _productService.GetById(id);
                if (products != null)
                {
                    return Ok(products);
                }
                else
                {
                    return NotFound(id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get product");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                ProductDto productDto = await _productService.AddAsync(request);
                return CreatedAtRoute(routeName: nameof(GetProductById),
                                      routeValues: new { id = productDto.Id },
                                      value: productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add product");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductRequest updateProductRequest)
        {
            try
            {                
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _productService.UpdateAsync(updateProductRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update product");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                await _productService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete product");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
    }
}
