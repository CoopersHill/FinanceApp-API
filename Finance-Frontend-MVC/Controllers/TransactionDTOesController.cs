using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Finance_Frontend_MVC.Data;
using hwFinanceApp.Models;

namespace Finance_Frontend_MVC.Controllers
{
    public class TransactionDTOesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IFinanceRepository _financeRepository;


        public TransactionDTOesController(IFinanceRepository financeRepository)
        {
            _financeRepository = financeRepository;
        }

        

        // GET: TransactionDTOes
        public async Task<IActionResult> Index()
        {
            var transactions = await _financeRepository.GetTransactionsAsync(null);
            return View(transactions);
        }

        // GET: TransactionDTOes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionDTO = await _financeRepository.GetTransactionsAsync((int)id);
                
            if (transactionDTO == null)
            {
                return NotFound();
            }

            return View(transactionDTO);
        }

        // GET: TransactionDTOes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TransactionDTOes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BankAccountID,Description,TransactionDate,Amount,Type,CategoryID,EnteredByID,RecStatus,ReconciledAmount,EnteredBy_ID")] TransactionDTO transactionDTO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transactionDTO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transactionDTO);
        }

        // GET: TransactionDTOes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionDTO = await _context.TransactionDTO.FindAsync(id);
            if (transactionDTO == null)
            {
                return NotFound();
            }
            return View(transactionDTO);
        }

        // POST: TransactionDTOes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ID,BankAccountID,Description,TransactionDate,Amount,Type,CategoryID,EnteredByID,RecStatus,ReconciledAmount,EnteredBy_ID")] TransactionDTO transactionDTO)
        {
            if (id != transactionDTO.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transactionDTO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionDTOExists(transactionDTO.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transactionDTO);
        }

        // GET: TransactionDTOes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionDTO = await _context.TransactionDTO
                .FirstOrDefaultAsync(m => m.ID == id);
            if (transactionDTO == null)
            {
                return NotFound();
            }

            return View(transactionDTO);
        }

        // POST: TransactionDTOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var transactionDTO = await _context.TransactionDTO.FindAsync(id);
            _context.TransactionDTO.Remove(transactionDTO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionDTOExists(long id)
        {
            return _context.TransactionDTO.Any(e => e.ID == id);
        }
        public string getCategory(int id) {
            string Category;
            return "";
        }
    }
}
