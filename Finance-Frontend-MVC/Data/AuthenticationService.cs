﻿using System;
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
        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;  
        
        }
        private string _APIToken { get; set; }
        private HttpClient _httpClient;
        public string authenticationURL;

        public async Task<TokenResponse> GetAPIToken()
        {
            

            var disco = await _httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
            }

            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }
            return tokenResponse;

        }
        public async Task<HttpClient> GetClient()
        {
            var tokenResponse = await GetAPIToken();
            if (tokenResponse != null)
            {
                _httpClient.SetBearerToken(tokenResponse.AccessToken);
                return _httpClient;
            }
            else
            { 
                return null;
            }

        }
    }

}