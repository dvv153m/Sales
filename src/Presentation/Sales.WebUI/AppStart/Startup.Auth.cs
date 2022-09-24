namespace Sales.WebUI.AppStart
{
    public partial class Startup
    {
        void ConfigureAuth(WebApplicationBuilder builder)
        {
            //CookieAuthenticationDefaults
            builder.Services.AddAuthentication("Cookie")
                .AddCookie("Cookie", config =>
                {
                    config.LoginPath = "/User/Index";
                });
            builder.Services.AddAuthorization();
        }
    }
}
