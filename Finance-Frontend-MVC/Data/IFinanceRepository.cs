using Finance_Frontend_MVC.Models;
using hwFinanceApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Finance_Frontend_MVC.Data
{
    public interface IFinanceRepository
    {
        Task<BankAccount> AddBankAccountAsync(BankAccount bankAccount);
        Task<bool> DeleteBankAccountAsync(int id);
        
        Task<IEnumerable<BankAccount>> GetBankAccountsAsync();
        Task<BankAccount> GetBankAccountsAsync(int? id);
        Task<BankAccountDTO> UpdateBankAccountAsync(int id, BankAccountDTO bankAccount);
        Task<TransactionDTO> GetTransactionsAsync(int? id);
        

    }
}