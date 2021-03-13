using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Finance_Frontend_MVC.Models;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace Finance_Frontend_MVC.Data
{
    public class FinanceRepository : IFinanceRepository
    {
        public FinanceRepository(HttpClient httpClient)
        {
            _apiClient = httpClient;
         

        }
        //private readonly string urlStub = "https://localhost:44325";
        private readonly string _bankAccountsEndPoint = "/api/BankAccountsAPI/";
        private HttpClient _apiClient;

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

        //set up client with token and URL
        public async void getConfiguredClient()
        {
            
            //var authenticationService = new AuthenticationService();
            TokenResponse sessionToken = await GetAPIToken(); //get our api token

            if (sessionToken.IsError)
            {
                Console.WriteLine("Failed to get API token");
                //return null;
            }

            //var apiClient = new HttpClient();  // set up new http client to configure
             _apiClient.SetBearerToken(sessionToken.AccessToken);
            //return  apiClient;
        }
        public async Task<String> getAPIRouteAsync(string routeSuffix)
        {
            var newEndpoint = String.Concat(_bankAccountsEndPoint,routeSuffix);
            return newEndpoint;
        }
        public async Task<IEnumerable<BankAccount>> GetBankAccountsAsync()
        {
            // getConfiguredClient();
            //var httpClient = await getConfiguredClient();
            TokenResponse sessionToken = await GetAPIToken();
            _apiClient.SetBearerToken(sessionToken.AccessToken);
            _apiClient.BaseAddress = null;
            // var requestUrl = await getAPIRouteAsync("");
            string urlStub = "";
            string requestUrl = urlStub + _bankAccountsEndPoint;
            if (_apiClient != null)
            {
                using (_apiClient)
                {
                    using (var response = await _apiClient.GetAsync("https://localhost:44325/api/BankAccountsAPI/") )
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var bankAccountsList = JsonConvert.DeserializeObject<IEnumerable<BankAccount>>(apiResponse);
                        return bankAccountsList.ToList();
                    }
                }
            }
            return null;
        }

        public async Task<BankAccount> GetBankAccountsAsync(int? id)
        {
            string requestUrl = "";
            if (id != null)
            {
                requestUrl += "/" + id; //get a single account
            }
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(requestUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var bankAccountsList = JsonConvert.DeserializeObject<BankAccount>(apiResponse);
                    return bankAccountsList;
                }
            }
        }



        public async Task<BankAccount> AddBankAccountAsync(BankAccount bankAccount)
        {
            string requestUrl ="";
            using (var httpClient = new HttpClient())
            {
                StringContent requestBody = new StringContent(JsonConvert.SerializeObject(bankAccount), System.Text.Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(requestUrl, requestBody))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    bankAccount = JsonConvert.DeserializeObject<BankAccount>(apiResponse);

                }
            }
            return bankAccount;
        }
        public async Task<BankAccount> UpdateBankAccountAsync(int id, BankAccount bankAccount)
        {
            BankAccount updatedBankAccount = new BankAccount();
            string requestUrl =  "/" + id;
            using (var httpClient = new HttpClient())
            {
                StringContent myBody = new StringContent(JsonConvert.SerializeObject(bankAccount), System.Text.Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(requestUrl, myBody))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    updatedBankAccount = JsonConvert.DeserializeObject<BankAccount>(apiResponse);
                }
            }


            return updatedBankAccount;
        }
        public async Task<bool> DeleteBankAccountAsync(int id)
        {

            string requestUrl = "\\" + id;
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.DeleteAsync(requestUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(apiResponse);
                }
            }


            return true;

        }
    }

}
