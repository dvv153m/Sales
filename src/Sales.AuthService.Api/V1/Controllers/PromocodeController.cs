﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sales.AuthService.Api.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocodeController : ControllerBase
    {
        /*public PromocodeController(IPromocodeService promocodeService)
        {

        }*/

        /// <summary>
        /// Вход по существующему промокоду
        /// </summary>
        /// <returns></returns>
        public bool Login()
        {
            //promocodeService.Login();
            return true;
        }

        /// <summary>
        /// Регистрируем новый промокод
        /// </summary>
        /// <returns></returns>
        public bool Register()
        {
            //при генерации проверить что такого промокода еще нет в бд
            //promocodeService.Register();
            return true;
        }
    }
}
