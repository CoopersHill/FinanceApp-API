using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hwFinanceApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public IEnumerable<Transaction> Transactions { get; set; }
        public int HouseholdId { get; set; }
        public HouseHold Household { get; set; }
         
    }
}
