using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;

namespace Finance_Frontend_MVC.Data
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService()
        {
        }
        private string _APIToken { get; set; }
        public async Task<TokenResponse> GetAPIToken()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                //var logger = services.GetRequiredService<ILogger<Program>>();
                //logger.LogError(disco.Error, "An error occurred getting the API token");                 
                Console.WriteLine(disco.Error);
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }
            Console.WriteLine(tokenResponse.Json);

            return tokenResponse;
        }
    }

}
