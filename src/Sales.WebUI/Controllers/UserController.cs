using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sales.Contracts.Configuration;
using Sales.WebUI.Models;
using System.Security.Claims;

namespace Sales.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly WebUIOptions _config;

        public UserController(IHttpClientFactory httpClientFactory,
                              IOptions<WebUIOptions> config)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

            if (config == null || config.Value == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Value;
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
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.PostAsync($"{_config.PromocodeApiUrl}/promocode/register", null);
                var newPromocode = await response.Content.ReadAsStringAsync();
                return await HandleResponse(newPromocode, response.IsSuccessStatusCode);                
            }
            catch (Exception ex)
            {
                //logger(ex)
                return await HandleResponse(promocode: String.Empty, isSuccessStatusCode: false);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel model)
        {                        
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var response = await httpClient.GetAsync($"{_config.PromocodeApiUrl}/promocode/exists/{model.Promocode}");
                return await HandleResponse(model.Promocode, response.IsSuccessStatusCode);                
            }
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
