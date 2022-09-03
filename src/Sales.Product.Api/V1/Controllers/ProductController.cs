﻿using Microsoft.AspNetCore.Mvc;
using Sales.Contracts.Request.Product;
using Sales.Core.Interfaces.Services;

namespace Sales.Product.Api.V1.Controllers
{
    [Route("api/v1/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService,
                                 ILogger<ProductController> logger)
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            try
            {
                await _productService.AddAsync(request);
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
