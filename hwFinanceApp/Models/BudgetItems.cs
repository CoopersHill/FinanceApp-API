using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hwFinanceApp.Models
{
    public class BudgetItem
    {
        public int ID { get; set; }
        public int CategoryId { get; set; }
        public int BudgetId { get; set; }
        public double Amount { get; set; }
        public int CategoriesId { get; set; }
    }
}
