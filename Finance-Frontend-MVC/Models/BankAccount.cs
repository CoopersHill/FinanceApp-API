using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Finance_Frontend_MVC.Models
{
    public class BankAccount
    {
        public BankAccount() {
            this.transactions = new HashSet<Transaction>();            
        }
        public int ID { get; set; } //primary key of Bank account
        [Display(Name = "Account Description")]
        public string AccountDescription { get; set; }
        [Display(Name = "Account Type")]
        public int AccountType { get; set; }

        [Display(Name = "Account Owner identification")]
        public int AccountOwnerId { get; set; } //User who owns the account
        
        [Display(Name = "Account Balance")]
        public double AccountBalance { get; set; }

        public virtual ICollection<Transaction> transactions { get; set; }

    }
}
