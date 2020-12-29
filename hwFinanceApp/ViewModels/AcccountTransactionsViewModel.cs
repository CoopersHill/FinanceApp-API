using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hwFinanceApp.Models;

namespace hwFinanceApp.ViewModels
{
    public class AcccountTransactionsViewModel
    {
    public  AcccountTransactionsViewModel(BankAccount bankAccount, List<Transaction> transactions)
        {
            this.bankAccount = bankAccount;
            this.transactions = transactions;
        }

        public BankAccount bankAccount { get; set; }
        public List<Transaction> transactions { get; set; }
    }


}
