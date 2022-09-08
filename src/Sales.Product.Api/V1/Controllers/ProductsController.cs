using Microsoft.AspNetCore.Mvc;
using Sales.Contracts.Models;
using Sales.Contracts.Request.Product;
using Sales.Core.Exceptions;
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
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));            
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {            
            try
            {
                var products = await _productService.GetAll();
                return Ok(products);
            }
            catch (ProductException ex)
            {
                _logger.LogError(ex, "Failed to get products");
                return BadRequest();
            }
        }
        
        /*[HttpGet]
        [Route("servicesbyproductids")]
        public async Task<IActionResult> GetByIds([FromQuery(Name = "ids")] int[] ids)
        {
            try
            {
                var products = await _productService.GetByIds(ids);
                return Ok(products);
            }
            catch (ProductException ex)
            {
                _logger.LogError(ex, "Failed to get products");
                return BadRequest();
            }
        }*/

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            try
            {                
                ProductDto productDto = await _productService.AddAsync(request);
                return CreatedAtRoute(routeName: nameof(GetProductById),
                                      routeValues: new { id = productDto.Id },
                                      value: productDto);
            }
            catch (ProductException ex)
            {
                _logger.LogError(ex, "Failed to add product");
                return BadRequest();
            }
        }

        [HttpGet("{id}", Name = nameof(GetProductById))]
        public async Task<IActionResult> GetProductById(long id)
        {
            try
            {
                var product = await _productService.GetById(id);
                if (product != null)
                {
                    return Ok(product);
                }
                else
                {
                    return NotFound(id);
                }
            }
            catch (ProductException ex)
            {
                _logger.LogError(ex, "Failed to get product");
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductRequest updateProductRequest)
        {
            try
            {                
                if (!ModelState.IsValid)//через fluent validation сделать
                {
                    return BadRequest();
                }

                await _productService.UpdateAsync(updateProductRequest);
            }
            catch (ProductException ex)
            {
                _logger.LogError(ex, "Failed to update product");
                return BadRequest();
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
            catch (ProductException ex)
            {
                _logger.LogError(ex, "Failed to delete product");
                return BadRequest();
            }
            return NoContent();
        }
    }
}
