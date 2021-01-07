using System;
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
                 new Transaction{ID = 2, BankAccountID = 1, Description = "Expense B", TransactionDate = DateTime.Parse("2020-03-03"), Amount = 88, Type = "Credit", CategoryID = 1, EnteredByID = "Hanif Warren", RecStatus = false, ReconciledAmount = 0, EnteredBy_ID = 1 },
                 new Transaction{ID = 3, BankAccountID = 2, Description = "Expense C", TransactionDate = DateTime.Parse("2020-07-31"), Amount = 50, Type = "Cash", CategoryID = 1, EnteredByID = "Hanif Warren", RecStatus = false, ReconciledAmount = 0, EnteredBy_ID = 1 },
                 new Transaction{ID = 3, BankAccountID = 2, Description = "Expense F", TransactionDate = DateTime.Parse("2020-07-31"), Amount = 50, Type = "Cash", CategoryID = 1, EnteredByID = "Hanif Warren", RecStatus = false, ReconciledAmount = 0, EnteredBy_ID = 1 },
                 new Transaction{ID = 3, BankAccountID = 3, Description = "Expense C", TransactionDate = DateTime.Parse("2020-07-31"), Amount = 50, Type = "Cash", CategoryID = 1, EnteredByID = "Hanif Warren", RecStatus = false, ReconciledAmount = 0, EnteredBy_ID = 1 },
                 new Transaction{ID = 3, BankAccountID = 3, Description = "Expense C", TransactionDate = DateTime.Parse("2020-07-31"), Amount = 50, Type = "Cash", CategoryID = 1, EnteredByID = "Hanif Warren", RecStatus = false, ReconciledAmount = 0, EnteredBy_ID = 1 },
            };
            var budgets = new Budget[] {
                new Budget{ID = 1, Name = "Food Budget", HouseHoldId = 1 },
                new Budget{ID = 2, Name = "Entertainment Budget", HouseHoldId = 1 },
                new Budget{ID = 3, Name = "Housing Budget", HouseHoldId = 2 },
                new Budget{ID = 4, Name = "Housing Budget", HouseHoldId = 2 },
                new Budget{ID = 5, Name = "Housing Budget", HouseHoldId = 3 },
                new Budget{ID = 6, Name = "Entertainment Budget", HouseHoldId = 3 },
            };
            foreach (var b in budgets) {
                context.Budgets.Add(b);
            }
            context.SaveChanges();

            var budgetItems = new BudgetItem[] {
            new BudgetItem{ ID = 1, CategoryId = 1, Amount = 100, CategoriesId = 1 },
            new BudgetItem{ ID = 2, CategoryId = 1, Amount = 100, CategoriesId = 2 },
            new BudgetItem{ ID = 3, CategoryId = 2, Amount = 100, CategoriesId = 3 },
            new BudgetItem{ ID = 4, CategoryId = 2, Amount = 100, CategoriesId = 1 },
            new BudgetItem{ ID = 5, CategoryId = 3, Amount = 100, CategoriesId = 2 },
            new BudgetItem{ ID = 6, CategoryId = 3, Amount = 100, CategoriesId = 3 },
            new BudgetItem{ ID = 7, CategoryId = 3, Amount = 100, CategoriesId = 1 }
            };
            foreach (var b in budgetItems) {
                context.BudgetItems.Add(b);
            }
            context.SaveChanges();
        }
    }
}
