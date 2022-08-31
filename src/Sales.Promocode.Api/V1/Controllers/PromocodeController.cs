using Microsoft.AspNetCore.Mvc;
using Sales.Core.Interfaces.Services;

namespace Sales.Promocode.Api.V1.Controllers
{
    [Route("api/v1/promocode")]
    [ApiController]
    public class PromocodeController : ControllerBase
    {
        private readonly IPromocodeService _promocodeService;
        private readonly ILogger<PromocodeController> _logger;

        public PromocodeController(IPromocodeService promocodeService,
                                   ILogger<PromocodeController> logger)
        {
            promocodeService = promocodeService ?? throw new ArgumentNullException(nameof(promocodeService));
            logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _promocodeService = promocodeService;
            _logger = logger;   
        }

        /// <summary>
        /// Определяет, существует ли заданный промокод
        /// </summary>
        /// <returns></returns>
        [HttpGet("exists/{promocode}")]
        public async Task<IActionResult> Exists(string promocode)
        {
            bool isExist = false;
            try
            {
                isExist = await _promocodeService.ExistsAsync(promocode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get promocode");
            }

            return isExist ? Ok() : NotFound();
        }

        /// <summary>
        /// Регистрирует новый промокод
        /// </summary>
        /// <returns>Возвращает созданный промокод</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register()
        {
            string newPromocode;
            try
            {
                newPromocode = await _promocodeService.AddPromocodeAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to register promocode");
                return BadRequest();
            }

            return CreatedAtRoute(routeName: String.Empty,
                                      //routeValues: new { promocode = newPromocode },
                                      value: newPromocode);
        }
        //return Ok(newPromocode);    
    }
}
