using Sales.Contracts.Configuration;

namespace Sales.Product.Api.AppStart
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
            builder.Services.Configure<ProductApiSettings>(builder.Configuration.GetSection(ProductApiSettings.SectionName));

            DbInitialize(builder);
            //ConfigureCors(builder);
            ConfigureServices(builder);
            //ConfigureSwagger(builder);
        }
    }
}
