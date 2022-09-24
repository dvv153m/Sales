namespace Sales.Promocode.Api.AppStart
{
    public partial class Startup
    {
        public readonly string DebugCorsPolicy = "DebugCorsPolicy";
        public readonly string ReleaseCorsPolicy = "ReleaseCorsPolicy";


        void ConfigureCors(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(DebugCorsPolicy, builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
                options.AddPolicy(ReleaseCorsPolicy, builder =>
                {
                    builder.WithOrigins("https://somedomain.ru")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }
    }
}
