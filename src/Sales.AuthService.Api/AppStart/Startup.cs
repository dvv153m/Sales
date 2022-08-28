using Sales.Contracts.Configuration;

namespace Sales.AuthService.Api.AppStart
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
            builder.Services.Configure<PromocodeServiceConfig>(builder.Configuration.GetSection(PromocodeServiceConfig.SectionName));

            DbInitialize(builder);
            //ConfigureAuth(builder);
            //ConfigureServices(builder);
        }
    }
}
