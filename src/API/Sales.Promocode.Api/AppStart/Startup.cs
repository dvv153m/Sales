using Sales.Contracts.Configuration;

namespace Sales.Promocode.Api.AppStart
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
            builder.Services.Configure<PromocodeApiOptions>(builder.Configuration.GetSection(PromocodeApiOptions.SectionName));

            DbInitialize(builder);
            ConfigureCors(builder);
            ConfigureServices(builder);
            ConfigureSwagger(builder);
        }
    }
}
