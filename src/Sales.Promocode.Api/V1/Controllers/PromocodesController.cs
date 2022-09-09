using Microsoft.AspNetCore.Mvc;
using Sales.Core.Interfaces.Services;

namespace Sales.Promocode.Api.V1.Controllers
{
    [Route("api/v1/promocodes")]
    [ApiController]
    public class PromocodesController : ControllerBase
    {
        private readonly IPromocodeService _promocodeService;
        private readonly ILogger<PromocodesController> _logger;

        public PromocodesController(IPromocodeService promocodeService,
                                   ILogger<PromocodesController> logger)
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
        [HttpGet("{promocode}")]
        public async Task<IActionResult> Get(string promocode)
        {                        
            Sales.Core.Domain.Promocode promocodeModel = await _promocodeService.GetByPromocodeAsync(promocode);
            return promocodeModel != null ? Ok(promocodeModel) : NotFound();
        }

        /// <summary>
        /// Регистрирует новый промокод
        /// </summary>
        /// <returns>Возвращает созданный промокод</returns>
        [HttpPost]
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
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtRoute(routeName: String.Empty,                                      
                                      value: newPromocode);
        }           
    }
}
