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
        public bool IsPurchanse { get; set; }

    }
}
