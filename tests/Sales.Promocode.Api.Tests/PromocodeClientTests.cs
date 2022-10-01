using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Moq.Protected;
using Sales.Infrastructure.Clients;
using Sales.Promocode.Api.AppStart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Promocode.Api.Tests
{
    public class PromocodeClientTests
    {
        private ProductClient _systemUnderTests;

        HttpClient _httpClient;

        public PromocodeClientTests()
        {
            //var appFactory = new WebApplicationFactory<Startup>();//нужно в конфигурацию добавить тестувую бд
            //_httpClient = appFactory.CreateClient();
        }

        [Fact]
        public void Test1()
        {
            try
            {
                var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
                HttpResponseMessage result = new HttpResponseMessage();
                handlerMock
    .Protected()
    .Setup<Task<HttpResponseMessage>>(
        "SendAsync",
        ItExpr.IsAny<HttpRequestMessage>(),
        ItExpr.IsAny<CancellationToken>()
    )
    .ReturnsAsync(result)
    .Verifiable();

                var httpClient = new HttpClient(handlerMock.Object);
                var mockHttpClientFactory = new Mock<IHttpClientFactory>();
                mockHttpClientFactory.Setup(s => s.CreateClient()).Returns(httpClient);

                PromocodeClient promocodeClient = new PromocodeClient(new HttpProxy(mockHttpClientFactory.Object), "https://localhost:7291/api/v1/promocodes");
                promocodeClient.GeneratePromocodeAsync().Wait();
            }
            catch (Exception ex)
            { 
            
            }
        }
    }
}
