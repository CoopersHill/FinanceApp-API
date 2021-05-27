using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hwFinanceApp.Models
{
    public class TransactionDTO
    {
        [Display(Name = "Transaction #")]
        public long ID { get; set; } //primary key of transaction
        public int BankAccountID { get; set; } //BankAccount to which the transaction belongs.

        [Display(Name = "Description")]
        public string Description { get; set; }
        
        [Display(Name = "Transaction Date")]
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; }
        
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public string Type { get; set; }
        public int CategoryID { get; set; }
        public string EnteredByID { get; set; }
        public bool RecStatus { get; set; }        
        
        [Display(Name = "Reconciled Amount")]
        [DataType(DataType.DateTime)]
        public double ReconciledAmount { get; set; }
        public int EnteredBy_ID { get; set; }
    }
}
