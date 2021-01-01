using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace hwFinanceApp.Models
{
    public class FinanceContext : DbContext
    {
        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options) 
        {         
        }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetItem> BudgetItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<HouseHold> HouseHolds { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>().ToTable("BankAccounts"); //only useful to override property name tables to force plural
            modelBuilder.Entity<Budget>().ToTable("Budgets");
            modelBuilder.Entity<BudgetItem>().ToTable("BudgetItems");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<HouseHold>().ToTable("Households");
            modelBuilder.Entity<Transaction>().ToTable("Transactions");
        }
        
    }
}
