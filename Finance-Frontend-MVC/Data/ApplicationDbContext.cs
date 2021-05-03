using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Finance_Frontend_MVC.Models;
using hwFinanceApp.Models;

namespace Finance_Frontend_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Finance_Frontend_MVC.Models.BankAccount> BankAccount { get; set; }
        public DbSet<hwFinanceApp.Models.BankAccountDTO> BankAccountDTO { get; set; }
    }
}
