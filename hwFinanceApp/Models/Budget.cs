using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hwFinanceApp.Models
{
    public class Budget
    {
        public int ID { get; set; }
        public string Name { get; set;  }
        public int HouseHoldId { get; set; }
        public virtual HouseHold Household { get; set; }

    }
}
