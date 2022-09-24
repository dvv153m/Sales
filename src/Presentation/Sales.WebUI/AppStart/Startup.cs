using Sales.Contracts.Configuration;

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
            //чтоб через di в конструкторе получать этот конфиг
            builder.Services.Configure<WebUIOptions>(builder.Configuration.GetSection(WebUIOptions.SectionName));

            ConfigureAuth(builder);
            ConfigureServices(builder);
        }
    }
}
