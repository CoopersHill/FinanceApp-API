using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hwFinanceApp.Models
{
    public class BankAccountDTO
    {
        public BankAccountDTO() {
            this.transactions = new HashSet<Transaction>();            
        }
        public BankAccountDTO(BankAccount b) {
            ID = b.ID;
            AccountDescription = b.AccountDescription;
            AccountType = b.AccountType;
            AccountOwnerId = b.AccountOwnerId;
            AccountBalance = b.AccountBalance;
        }
        public BankAccount Convert() {
            var bankAccount = new BankAccount()
            {
                ID = this.ID,
                AccountDescription = this.AccountDescription,
                AccountType = this.AccountType,
                AccountOwnerId = this.AccountOwnerId,
                AccountBalance = this.AccountBalance
            };
            return bankAccount;
        }
        public int ID { get; set; } //primary key of Bank account
        public string AccountDescription { get; set; }
        public int AccountType { get; set; }
        public int AccountOwnerId { get; set; } //User who owns the account
        public double AccountBalance { get; set; }

        public virtual ICollection<Transaction> transactions { get; set; }

    }
}
