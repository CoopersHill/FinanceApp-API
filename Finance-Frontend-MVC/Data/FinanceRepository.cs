using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Finance_Frontend_MVC.Models;
using Newtonsoft.Json;

namespace Finance_Frontend_MVC.Data
{
    public class FinanceRepository : IFinanceRepository
    {
        public FinanceRepository()
        {
        }
        private readonly string urlStub = "https://localhost:44325";
        private readonly string bankAccountsEndPoint = "/api/BankAccountsAPI";        

        public async Task<IEnumerable<BankAccount>> GetBankAccountsAsync()
        {            
            string requestUrl = urlStub + bankAccountsEndPoint;            
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(requestUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var bankAccountsList = JsonConvert.DeserializeObject<IEnumerable<BankAccount>>(apiResponse);
                   return bankAccountsList.ToList();
                }
            }
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



        public async Task<BankAccount> AddBankAccountAsync(BankAccount bankAccount) {
            string requestUrl = urlStub + bankAccountsEndPoint;
             using (var httpClient = new HttpClient()) {
                StringContent requestBody = new StringContent(JsonConvert.SerializeObject(bankAccount), System.Text.Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(requestUrl, requestBody)) {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    bankAccount =  JsonConvert.DeserializeObject<BankAccount>(apiResponse);

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
        public async Task<bool> DeleteBankAccountAsync(int id) {

            string requestUrl = urlStub + bankAccountsEndPoint + id;
            using (var httpClient = new HttpClient()) {

                using (var response = await httpClient.DeleteAsync(requestUrl)) {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(apiResponse);
                }
            }
            
            
            return true;

        }
    }

}
