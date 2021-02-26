using Finance_Frontend_MVC.Models;
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
        Task<BankAccount> UpdateBankAccountAsync(int id, BankAccount bankAccount);
        
    }
}