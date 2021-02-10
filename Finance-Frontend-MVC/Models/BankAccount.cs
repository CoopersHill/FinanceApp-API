﻿using System;
using System.Collections.Generic;
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
        public string AccountDescription { get; set; }
        public int AccountType { get; set; }
        public int AccountOwnerId { get; set; } //User who owns the account
        public double AccountBalance { get; set; }

        public virtual ICollection<Transaction> transactions { get; set; }

    }
}
