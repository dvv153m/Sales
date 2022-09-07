using Microsoft.AspNetCore.Mvc;
using Sales.Contracts.Models;
using Sales.Contracts.Request.Order;
using Sales.Core.Exceptions;
using Sales.Core.Interfaces.Services;

namespace Sales.Order.Api.V1.Controllers
{
    [Route("api/v1/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService,
                                ILogger<OrdersController> logger)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderRequest request)
        {
            try
            {
                OrderDto orderDto = await _orderService.AddAsync(request);
                return CreatedAtRoute(routeName: nameof(GetOrderById),
                                      routeValues: new { id = orderDto.Id },
                                      value: orderDto);
            }
            catch (OrderException ex)
            {
                _logger.LogError(ex, $"Failed to create order. {ex.Message}");
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}", Name = nameof(GetOrderById))]
        public async Task<IActionResult> GetOrderById(long id)
        {
            try
            {
                var order = await _orderService.GetById(id);
                if (order != null)
                {
                    return Ok(order);
                }
                else
                {
                    return NotFound(id);
                }
            }
            catch (ProductException ex)
            {
                _logger.LogError(ex, $"Failed to get order. {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
