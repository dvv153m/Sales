using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.Core.Interfaces.Services;

namespace Sales.Promocode.Api.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocodeController : ControllerBase
    {
        public PromocodeController(IPromocodeService promocodeService)
        {

        }

        /// <summary>
        /// Вход по существующему промокоду
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool Login()
        {
            //promocodeService.Login();
            return true;
        }

        /// <summary>
        /// Регистрируем новый промокод
        /// </summary>
        /// <returns></returns>
        /*public bool Register()
        {
            //при генерации проверить что такого промокода еще нет в бд
            //promocodeService.Register();
            return true;
        }*/
    }
}
