using Microsoft.AspNetCore.Mvc.Testing;
using Sales.Promocode.Api.AppStart;
using System.Net.Http.Json;

namespace Sales.Promocode.Api.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient _httpClient;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        //тестовая DB or inmemoryDB
                        //services.addContext
                    });
                });
            _httpClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(scheme: "bearer",
                                                                                                                    parameter: await GetJwtToken());
        }

        private async Task<string> GetJwtToken()
        {
            /*var response = _httpClient.PostAsJsonAsync("api/v1/promocodes", new UserRegistrationRequest);

            var registrationResponse = await response.Context.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;*/

            return await Task.FromResult("1212");
        }
    }
}
