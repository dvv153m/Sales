using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Sales.WebUI.Middleware
{
    public static class GlobalExceptionHandlerMiddlewareExtensions
    {
        public static void UseGlobalExceptionHandlerMiddleware(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "text/html";

                    await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                    await context.Response.WriteAsync("ERROR!<br><br>\r\n");

                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    if (exceptionHandlerPathFeature != null)
                    {
                        logger.LogError($"Something went wrong: {exceptionHandlerPathFeature}");

                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "An internal server has occurred"
                        }.ToString());
                    }
                    
                    await context.Response.WriteAsync(
                                                  "<a href=\"/\">Home</a><br>\r\n");
                    await context.Response.WriteAsync("</body></html>\r\n");
                    await context.Response.WriteAsync(new string(' ', 512));                    
                });
            });
        }        
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
