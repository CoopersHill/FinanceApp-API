﻿using System;
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
       public FinanceRepository(IAuthenticationService authenticationService)
        {
           
            _authenticationService = authenticationService;         

        }
        private readonly string _bankAccountsEndPoint = "/api/BankAccountsAPI/";
        private HttpClient _apiClient;
        private IAuthenticationService _authenticationService;

        
        public async Task<String> getAPIRouteAsync(string routeSuffix)
        {
            var newEndpoint = String.Concat(_bankAccountsEndPoint,routeSuffix);
            return newEndpoint;
        }

        public async Task<IEnumerable<BankAccount>> GetBankAccountsAsync()
        {
           
            _apiClient = await _authenticationService.GetClient();         
            
            
            string urlStub = "";
            string requestUrl = urlStub + _bankAccountsEndPoint;
            if (_apiClient != null)
            {
                using (_apiClient)
                {
                    using (var response = await _apiClient.GetAsync("https://localhost:44325/api/BankAccountsAPI/"))
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
