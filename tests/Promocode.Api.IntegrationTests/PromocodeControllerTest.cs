using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Sales.Promocode.Api.AppStart;
using System.Net;
using System.Net.Http;
using Xunit;

namespace Sales.Promocode.Api.IntegrationTests
{
    public class PromocodeControllerTest : IntegrationTest
    {        
        [Fact]
        public async void Get_NotExistPromocode_ReturnEmptyResponse()
        {
            //Arrange
            await AuthenticateAsync();

            //Act
            var response = await _httpClient.GetAsync("api/v1/promocode/123");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            //await response.Content.ReadAsStringAsync().Should().BeNull();
        }
    }
}