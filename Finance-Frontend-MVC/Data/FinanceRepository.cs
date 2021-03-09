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
        public FinanceRepository()
        {
        }
        private readonly string urlStub = "https://localhost:44325";
        private readonly string bankAccountsEndPoint = "/api/BankAccountsAPI/";

        //set up client with token and URL
        public async Task<HttpClient> getConfiguredClient()
        {
            var authenticationService = new AuthenticationService();
            TokenResponse sessionToken = await authenticationService.GetAPIToken(); //get our api token

            if (sessionToken.IsError)
            {
                Console.WriteLine("Failed to get API token");
                return null;
            }

            var apiClient = new HttpClient();  // set up new http client to configure
            apiClient.SetBearerToken(sessionToken.AccessToken);
            return apiClient;
        }
        public async Task<String> getAPIRouteAsync(string routeSuffix)
        {
            var newEndpoint = String.Concat(urlStub, bankAccountsEndPoint, routeSuffix);
            return newEndpoint;
        }
        public async Task<IEnumerable<BankAccount>> GetBankAccountsAsync()
        {
            var httpClient = await getConfiguredClient();
            var requestUrl = await getAPIRouteAsync("");
            //string requestUrl = urlStub + bankAccountsEndPoint;
            if (httpClient != null)
            {
                using (httpClient)
                {
                    using (var response = await httpClient.GetAsync(requestUrl))
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
            string requestUrl = urlStub + bankAccountsEndPoint;
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
            string requestUrl = urlStub + bankAccountsEndPoint;
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
            string requestUrl = urlStub + bankAccountsEndPoint + "/" + id;
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

            string requestUrl = urlStub + bankAccountsEndPoint + id;
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
