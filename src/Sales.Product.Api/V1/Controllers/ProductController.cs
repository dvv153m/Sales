﻿using Microsoft.AspNetCore.Mvc;
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
                var res = _productService.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get promocode");
            }

            return Ok();
        }
    }
}
