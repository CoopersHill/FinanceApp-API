using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hwFinanceApp.Models
{
    public class CategoryDTO
    {
        public CategoryDTO(Category category) {
            this.ID = category.ID;
            this.Name = category.Name;
            this.HouseholdID = category.HouseholdID;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        
        //public IEnumerable<Transaction> Transactions { get; set; }
        public int HouseholdID { get; set; }
      
         
    }
}
