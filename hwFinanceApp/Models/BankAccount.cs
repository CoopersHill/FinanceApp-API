using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hwFinanceApp.Models
{
    public class BankAccount
    {
        public BankAccount() {
            this.Transactions = new HashSet<Transaction>();            
        }
        public int Id { get; set; } //primary key of Bank account
        public string AccountDescription { get; set; }
        public int AccountType { get; set; }
        public int AccountOwnerId { get; set; } //User who owns the account
        public double AccountBalance { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}
