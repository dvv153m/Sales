using Sales.WebUI.Configuration;

namespace Sales.WebUI.AppStart
{
    public partial class Startup
    {
        /// <summary>
        /// Инициализация компонентов приложения
        /// </summary>
        /// <param name="builder"></param>
        public void Initialize(WebApplicationBuilder builder)
        {
            //чтоб через di в конструкторе контроллера получать этот конфиг
            builder.Services.Configure<AppConfig>(builder.Configuration.GetSection(AppConfig.SectionName));

            ConfigureAuth(builder);
            ConfigureServices(builder);
        }
    }
}
