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
    public class TransactionsController : ControllerBase
    {
        private readonly FinanceContext _context;

        public TransactionsController(FinanceContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        //// GET: api/Transactions
        //[HttpGet("{id}")]
        //public async Task<ActionResult<IEnumerable<Transaction>>> GetAccountTransactions(long id)
        //{
        //    return await _context.Transactions.Where( g => g.OwnerAccountId == id).ToListAsync();
        //}

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(long ID)
        {
            var transaction = await _context.Transactions.FindAsync(ID);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{ID}")]
        public async Task<IActionResult> PutTransaction(long id, Transaction transaction)
        {
            if (id != transaction.ID)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            Console.Write(_context.Transactions.Count());
            transaction.TransactionDate = DateTime.Now;
            _context.Transactions.Add(transaction);
            Console.WriteLine(_context.Transactions.Count());
            //var bankAccount = await _context.BankAccounts.FindAsync(transaction.BankAccountId); //find the account this belongs to
            //if (bankAccount != null)
            //{
            //    bankAccount.Transactions = _context.Transactions.Where(g => g.BankAccountId == bankAccount.Id).ToList();
            //    bankAccount.AccountBalance = bankAccount.Transactions.Select(h => h.ItemCost).Sum();
            //_context.Entry(bankAccount).State = EntityState.Modified;
            //}

            await _context.SaveChangesAsync();
            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{                   
            //                        throw;                
            //}
           

           // return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.ID }, transaction);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(long id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(long id)
        {
            return _context.Transactions.Any(e => e.ID == id);
        }
    }
}
