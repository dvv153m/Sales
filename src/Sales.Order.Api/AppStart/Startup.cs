using Sales.Contracts.Configuration;

namespace Sales.Order.Api.AppStart
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
            builder.Services.Configure<OrderApiOptions>(builder.Configuration.GetSection(OrderApiOptions.SectionName));

            ConfigureServices(builder);
            //DbInitialize(builder);
            /*ConfigureCors(builder);
            ConfigureServices(builder);
            ConfigureSwagger(builder);*/
        }
    }
}
