﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hwFinanceApp.Models;


namespace hwFinanceApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FinanceContext context)
        {
            context.Database.EnsureCreated();

            //look for any accounts
            if (context.HouseHolds.Any())
            {
                return; //db has been created
            }

            var households = new HouseHold[] { 
            new HouseHold{ID = 1, HouseholdName = "Household 1", HeadOfHouseholdID = 1, Categories = null, Budgets = null},
            new HouseHold{ID = 2, HouseholdName = "Household 2", HeadOfHouseholdID = 2, Categories = null, Budgets = null},
            new HouseHold{ID = 3, HouseholdName = "Household 3", HeadOfHouseholdID = 3, Categories = null, Budgets = null},
            };
            foreach (var h in households) {
                context.HouseHolds.Add(h);
            }
            context.SaveChanges();

            var bankAccounts = new BankAccount[] {
                new BankAccount{ID = 1, AccountDescription = "Basic Checking Account", AccountType = 1, AccountOwnerId = 1, AccountBalance = 1000  },
                new BankAccount{ID = 2, AccountDescription = "Basic Savings Account", AccountType = 2, AccountOwnerId = 1, AccountBalance = 2000  },
                new BankAccount{ID = 3, AccountDescription = "Basic Retirement Account", AccountType = 2, AccountOwnerId = 1, AccountBalance = 3000  },
            };
            foreach (var b in bankAccounts) {
                context.BankAccounts.Add(b);
            }
            context.SaveChanges();

            var transactions = new Transaction[] {
                new Transaction{ID = 1, BankAccountID = 1, Description = "Expense a", TransactionDate = DateTime.Parse("2020-07-06"), Amount = 99, Type = "Debit", CategoryID = 1, EnteredByID = "Hanif Warren", RecStatus = false, ReconciledAmount = 0, EnteredBy_ID = 1 },
                 new Transaction{ID = 2, BankAccountID = 2, Description = "Expense B", TransactionDate = DateTime.Parse("2020-03-03"), Amount = 88, Type = "Credit", CategoryID = 1, EnteredByID = "Hanif Warren", RecStatus = false, ReconciledAmount = 0, EnteredBy_ID = 1 },
                 new Transaction{ID = 3, BankAccountID = 2, Description = "Expense C", TransactionDate = DateTime.Parse("2020-07-31"), Amount = 50, Type = "Cash", CategoryID = 1, EnteredByID = "Hanif Warren", RecStatus = false, ReconciledAmount = 0, EnteredBy_ID = 1 }
            };


        }
    }
}
