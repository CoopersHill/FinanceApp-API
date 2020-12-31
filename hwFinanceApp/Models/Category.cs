using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hwFinanceApp.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        
        public IEnumerable<Transaction> Transactions { get; set; }
        public int HouseholdID { get; set; }
      
         
    }
}
