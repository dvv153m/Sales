using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sales.Core.Domain;
using Sales.Core.Interfaces.Clients;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Sales.Infrastructure.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IPromocodeClient _promocodeClient;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                                          ILoggerFactory logger,
                                          UrlEncoder encoder,
                                          ISystemClock clock,
                                          IPromocodeClient promocodeClient) : base(options, logger, encoder, clock)
        {
            _promocodeClient = promocodeClient ?? throw new ArgumentNullException(nameof(promocodeClient));
        }


        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {                        
            if (Request.Headers.ContainsKey("Authorization"))
            {
                Promocode promocode = null;
                try
                {
                    var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                    promocode = await _promocodeClient.GetByPromocodeAsync(promocode: authHeader.ToString());
                }
                catch (System.Exception ex)
                {
                    Response.StatusCode = 401;
                    return AuthenticateResult.Fail($"Authentication failed: {ex.Message}");
                }

                if (promocode != null && !string.IsNullOrEmpty(promocode.Value))
                {
                    var claims = new[] {
                        new Claim(ClaimTypes.NameIdentifier, promocode.Value),
                    };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
            }
            Response.StatusCode = 401;
            return AuthenticateResult.Fail("Authorization Header not found");
        }
    }
}
