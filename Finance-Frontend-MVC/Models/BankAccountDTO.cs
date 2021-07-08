using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hwFinanceApp.Models
{
    public class BankAccountDTO
    {
        public BankAccountDTO() {
            this.transactions = new HashSet<TransactionDTO>();            
        }
        
        
        public int ID { get; set; } //primary key of Bank account
        public string AccountDescription { get; set; }
        public int AccountType { get; set; }
        public int AccountOwnerId { get; set; } //User who owns the account
        public double AccountBalance { get; set; }

        public ICollection<TransactionDTO> transactions { get; set; }

    }
}
