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
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //look for any accounts
            if (context.HouseHolds.Any())
            {
                return; //db has been created
            }

            var households = new HouseHold[] {
            new HouseHold{ HouseholdName = "Warren Family", HeadOfHouseholdID = 1, Categories = null, Budgets = null},
            new HouseHold{ HouseholdName = "Waller family", HeadOfHouseholdID = 2, Categories = null, Budgets = null},
            new HouseHold{ HouseholdName = "HAW Consulting", HeadOfHouseholdID = 3, Categories = null, Budgets = null},
            };
            foreach (var h in households) {
                context.HouseHolds.Add(h);
            }
            var result = context.SaveChanges();

            var Categories = new Category[] {
            new Category{ Name = "Housing"},
            new Category { Name = "Food" },
            new Category { Name = "Transportation"},
            new Category { Name = "Entertainment"},
            new Category{ Name = "Clothing"}
            };

            //get list of Household IDs, sorted
            var houseHoldIDs = context.HouseHolds.Select(h => h.ID).ToArray();
            var random = new Random();
            foreach (var c in Categories) {

                c.HouseholdID = houseHoldIDs[random.Next(houseHoldIDs.Length)]; //get household ID at random index
                context.Categories.Add(c);
            }
             
            result = context.SaveChanges();

            var bankAccounts = new BankAccount[] {
                new BankAccount{ AccountDescription = "Basic Checking Account", AccountType = 1, AccountOwnerId = 1, AccountBalance = 1000  },
                new BankAccount{ AccountDescription = "Basic Savings Account", AccountType = 2, AccountOwnerId = 1, AccountBalance = 2000  },
                new BankAccount{ AccountDescription = "Basic Retirement Account", AccountType = 2, AccountOwnerId = 1, AccountBalance = 3000  },
            };
            foreach (var b in bankAccounts) {    
                
                context.BankAccounts.Add(b);
            }
            result = context.SaveChanges();

            var transactions = new Transaction[] {
                new Transaction{ BankAccountID = 1, Description = "Expense a", TransactionDate = DateTime.Parse("2020-07-06"), Amount = 99, Type = "Debit",   EnteredByID = "Hanif Warren", RecStatus = false, ReconciledAmount = 0, EnteredBy_ID = 1, CategoryID = 1 }
                ,
                 new Transaction{ BankAccountID = 1, Description = "Expense B", TransactionDate = DateTime.Parse("2020-03-03"), Amount = 88, Type = "Credit",   EnteredByID = "Hanif Warren", RecStatus = false, ReconciledAmount = 0, EnteredBy_ID = 1 },
                 new Transaction{ BankAccountID = 2, Description = "Expense C", TransactionDate = DateTime.Parse("2020-07-31"), Amount = 50, Type = "Cash",   EnteredByID = "Hanif Warren", RecStatus = false, ReconciledAmount = 0, EnteredBy_ID = 1 },
                 new Transaction{ BankAccountID = 2, Description = "Expense F", TransactionDate = DateTime.Parse("2020-07-31"), Amount = 50, Type = "Cash",   EnteredByID = "Hanif Warren", RecStatus = false, ReconciledAmount = 0, EnteredBy_ID = 1 },
                 new Transaction{ BankAccountID = 3, Description = "Expense C", TransactionDate = DateTime.Parse("2020-07-31"), Amount = 50, Type = "Cash",   EnteredByID = "Hanif Warren", RecStatus = false, ReconciledAmount = 0, EnteredBy_ID = 1 },
                 new Transaction{ BankAccountID = 3, Description = "Expense C", TransactionDate = DateTime.Parse("2020-07-31"), Amount = 50, Type = "Cash",   EnteredByID = "Hanif Warren", RecStatus = false, ReconciledAmount = 0, EnteredBy_ID = 1 }
            };
                var BankAccountIDs = context.BankAccounts.Select(b => b.ID).ToArray();
            foreach (var tran in transactions) {
                var idToAdd = BankAccountIDs[random.Next(BankAccountIDs.Length)]; // assign random existing bankAccount

                tran.BankAccountID = idToAdd;
            context.Transactions.Add(tran);
            }
            result = context.SaveChanges();
            var budgets = new Budget[] {
                new Budget{ Name = "Food Budget", HouseHoldId = 1 },
                new Budget{ Name = "Entertainment Budget", HouseHoldId = 1 },
                new Budget{ Name = "Housing Budget", HouseHoldId = 2 },
                new Budget{ Name = "Housing Budget", HouseHoldId = 2 },
                new Budget{ Name = "Housing Budget", HouseHoldId = 3 },
                new Budget{ Name = "Entertainment Budget", HouseHoldId = 3 },
            };
            foreach (var b in budgets) {
                context.Budgets.Add(b);
            }
            result = context.SaveChanges();

            //var budgetItems = new BudgetItem[] {
            //new BudgetItem{ Amount = 100, CategoriesId = 1 },
            //new BudgetItem{ Amount = 100, CategoriesId = 2 },
            //new BudgetItem{ Amount = 100, CategoriesId = 3 },
            //new BudgetItem{ Amount = 100, CategoriesId = 1 },
            //new BudgetItem{ Amount = 100, CategoriesId = 2 },
            //new BudgetItem{ Amount = 100, CategoriesId = 3 },
            //new BudgetItem{ Amount = 100, CategoriesId = 1 }
            //};
            //foreach (var b in budgetItems) {
            //    context.BudgetItems.Add(b);
            //}
            //result = context.SaveChanges();
        }
    }
}
