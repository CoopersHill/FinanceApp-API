using Finance_Frontend_MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance_Frontend_MVC.Data
{
    public interface IFinanceRepository
    {
        Task<List<BankAccount>> GetBankAccountsAsync();
        Task<BankAccount> UpdateBankAccountAsync(int id, BankAccount bankAccount);
    }
}