﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hwFinanceApp.Models
{
    public class HouseHold
    {
       public int ID { get; set; }
       public string HouseholdName { get; set; }
       public int HeadOfHouseholdID { get; set; }
       public virtual IEnumerable<BankAccount> houseHoldAccounts { get; set; }
       public ICollection<Category> Categories { get; set; }
       public ICollection<Budget> Budgets { get; set; }
    }
}
