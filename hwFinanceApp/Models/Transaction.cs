using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hwFinanceApp.Models
{
    public class Transaction
    {
        public long ID { get; set; } //primary key of transaction
        public int BankAccountID { get; set; } //Id to which the transaction belongs.
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }
        public int CategoryID { get; set; }
        public string EnteredByID { get; set; }
        public bool RecStatus { get; set; }        
        public double ReconciledAmount { get; set; }
        public int EnteredBy_ID { get; set; }
    }
}
