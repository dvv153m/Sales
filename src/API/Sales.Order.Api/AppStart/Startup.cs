﻿using Sales.Contracts.Configuration;

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
            builder.Services.Configure<OrderApiSettings>(builder.Configuration.GetSection(OrderApiSettings.SectionName));

            builder.Services.AddControllers();

            DbInitialize(builder);
            ConfigureAuth(builder);
            ConfigureServices(builder);
            //ConfigureCors(builder);                        
            ConfigureSwagger(builder);
        }
    }
}
