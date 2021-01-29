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
    public class BudgetItemsController : ControllerBase
    {
        private readonly FinanceContext _context;

        public BudgetItemsController(FinanceContext context)
        {
            _context = context;
        }

        // GET: api/BudgetItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BudgetItem>>> GetBudgetItems()
        {
            return await _context.BudgetItems.ToListAsync();
        }

        // GET: api/BudgetItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BudgetItem>> GetBudgetItem(int id)
        {
            var budgetItem = await _context.BudgetItems.FindAsync(id);

            if (budgetItem == null)
            {
                return NotFound();
            }

            return budgetItem;
        }

        // PUT: api/BudgetItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudgetItem(int id, BudgetItem budgetItem)
        {
            if (id != budgetItem.ID)
            {
                return BadRequest();
            }

            _context.Entry(budgetItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BudgetItemExists(id))
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

        // POST: api/BudgetItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BudgetItem>> PostBudgetItem(BudgetItem budgetItem)
        {
            _context.BudgetItems.Add(budgetItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBudgetItem", new { id = budgetItem.ID }, budgetItem);
        }

        // DELETE: api/BudgetItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudgetItem(int id)
        {
            var budgetItem = await _context.BudgetItems.FindAsync(id);
            if (budgetItem == null)
            {
                return NotFound();
            }

            _context.BudgetItems.Remove(budgetItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BudgetItemExists(int id)
        {
            return _context.BudgetItems.Any(e => e.ID == id);
        }
    }
}
