using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hwFinanceApp.Models;

namespace hwFinanceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsAPIController : ControllerBase
    {
        private readonly FinanceContext _context;

        public BankAccountsAPIController(FinanceContext context)
        {
            _context = context;
        }

        // GET: api/BankAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccount>>> GetBankAccounts()
        {
            var bankAccounts  = await _context.BankAccounts.ToListAsync();
            foreach (var account in bankAccounts) {
                account.transactions = await  _context.Transactions.Where(g => g.BankAccountID == account.ID).ToListAsync();
            }

            return bankAccounts;
        }

        // GET: api/BankAccounts/5
        [HttpGet("{ID}")]
        public async Task<ActionResult<BankAccount>> GetBankAccount(int ID)
        {
            var bankAccount = await _context.BankAccounts.FindAsync(ID);
            bankAccount.transactions = _context.Transactions.Where(g => g.BankAccountID == ID).ToList();
            bankAccount.AccountBalance = bankAccount.transactions.Select(h => h.Amount).Sum();

            if (bankAccount == null)
            {
                return NotFound();
            }

            return bankAccount;
        }
        

        // PUT: api/BankAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{ID}")]
        public async Task<IActionResult> PutBankAccount(int ID, BankAccount bankAccount)
        {
            if (ID != bankAccount.ID)
            {
                return BadRequest();
            }
            var transactions =  _context.Transactions.Where(g => g.BankAccountID == ID).ToList();
            bankAccount.transactions = transactions;

            _context.Entry(bankAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankAccountExists(ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BankAccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BankAccount>> PostBankAccount(BankAccount bankAccount)
        {
            _context.BankAccounts.Add(bankAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBankAccount", new { ID = bankAccount.ID }, bankAccount);
        }

        // DELETE: api/BankAccounts/5
        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteBankAccount(int ID)
        {
            var bankAccount = await _context.BankAccounts.FindAsync(ID);
            if (bankAccount == null)
            {
                return NotFound();
            }

            _context.BankAccounts.Remove(bankAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BankAccountExists(int ID)
        {
            return _context.BankAccounts.Any(e => e.ID == ID);
        }
    }
}
