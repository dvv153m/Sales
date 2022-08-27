using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Sales.WebUI.Models;
using System.Security.Claims;

namespace Sales.WebUI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel model)
        {
            //обращаемся к сервису аутентификации
            //там создается новая запись в бд с промокодом

            var claims = new List<Claim>
            {
                new Claim("Promocode", model.Promocode)
            };
            var claimIdentity = new ClaimsIdentity(claims, "Cookie");
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync("Cookie", claimPrincipal);

            return Redirect(model.ReturnUrl ?? "/");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
