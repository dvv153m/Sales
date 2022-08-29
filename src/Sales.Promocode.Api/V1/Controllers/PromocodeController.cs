using Microsoft.AspNetCore.Mvc;
using Sales.Core.Interfaces.Services;

namespace Sales.Promocode.Api.V1.Controllers
{
    [Route("api/[controller]")]
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
        /// Вход по существующему промокоду
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login(string promocode)
        {
            var isSuccess = _promocodeService.LoginByPromocode(promocode);
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
        public IActionResult Register()
        {
            var isSuccess = _promocodeService.AddPromocode();
            if (!isSuccess)
            {
                return BadRequest("error");
            }
            return Ok();
        }
    }
}
