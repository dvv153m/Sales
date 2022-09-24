using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Sales.Core.Interfaces.Clients;
using Sales.WebUI.Models;
using System.Security.Claims;

namespace Sales.WebUI.Controllers
{
    public class UserController : Controller
    {        
        private readonly IPromocodeClient _promocodeClient;        

        public UserController(IPromocodeClient promocodeClient)                                                               
        {
            _promocodeClient = promocodeClient ?? throw new ArgumentNullException(nameof(promocodeClient));            
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Генерация промокода и аутентификация пользователя в системе
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GeneratePromocode()
        {
            var newPromocode = await _promocodeClient.GeneratePromocodeAsync();
            return await HandleResponse(newPromocode.Value, true);            
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel model)
        {      
            var promocode = await _promocodeClient.GetByPromocodeAsync(model.Promocode);            
            return await HandleResponse(model.Promocode, isSuccessStatusCode: promocode != null);            
        }

        private async Task<IActionResult> HandleResponse(string promocode, bool isSuccessStatusCode)
        {
            if (isSuccessStatusCode)
            {
                var claims = new List<Claim>
                    {
                        new Claim("Promocode", promocode)
                    };
                var claimIdentity = new ClaimsIdentity(claims, "Cookie");
                var claimPrincipal = new ClaimsPrincipal(claimIdentity);
                await HttpContext.SignInAsync("Cookie", claimPrincipal);

                return Redirect("/");
            }
            else
            {
                return Redirect("/User/Index");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
