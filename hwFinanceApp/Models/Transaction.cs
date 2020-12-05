using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hwFinanceApp.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public string ItemName { get; set; }
        public double ItemCost { get; set; }
        public bool RecStatus { get; set; }
        public DateTime TransactionDate { get; set; }
        public int OwnerAccountId { get; set; }
    }
}
