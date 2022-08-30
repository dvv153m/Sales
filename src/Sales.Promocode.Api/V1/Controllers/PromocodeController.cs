using Microsoft.AspNetCore.Mvc;
using Sales.Core.Interfaces.Services;

namespace Sales.Promocode.Api.V1.Controllers
{
    [Route("api/v1/promocode")]
    [ApiController]
    public class PromocodeController : ControllerBase
    {
        private readonly IPromocodeService _promocodeService;

        public PromocodeController(IPromocodeService promocodeService)
        {
            if(promocodeService == null)
                throw new ArgumentNullException(nameof(promocodeService));

            _promocodeService = promocodeService;
        }

        /// <summary>
        /// Вход по промокоду
        /// </summary>
        /// <returns></returns>
        [HttpGet("{promocode}")]
        public async Task<IActionResult> Get(string promocode)
        {
            var isSuccess = await _promocodeService.GetByPromocodeAsync(promocode);
            if (!isSuccess)
            {
                return NotFound();
            }            
            return Ok();
        }

        /// <summary>
        /// Регистрируем новый промокод
        /// </summary>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register()
        {
            var isSuccess = await _promocodeService.AddPromocodeAsync();
            if (!isSuccess)
            {
                return BadRequest("error");
            }
            return Ok();
        }
    }
}
