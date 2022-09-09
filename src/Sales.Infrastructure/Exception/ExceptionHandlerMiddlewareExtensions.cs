using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Sales.Infrastructure.Exception
{
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetail
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal server error"
                        }.ToString());
                    }
                });
            });
        }

        public class ErrorDetail
        {
            public int StatusCode { get; set; }

            public string Message { get; set; }

            public override string ToString()
            {
                return JsonSerializer.Serialize(this);
            }
        }
    }
}
