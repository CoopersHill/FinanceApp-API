using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Finance_Frontend_MVC.Data;
using Finance_Frontend_MVC.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace Finance_Frontend_MVC.Controllers
{
    public class BankAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string urlStub = "https://localhost:44325";
        private readonly string bankAccountsEndPoint = "/api/BankAccounts";

        private IFinanceRepository _financeRepository;
      

        public BankAccountsController(IFinanceRepository financeRepository) {
            _financeRepository = financeRepository;
        }

        // GET: BankAccounts
        public async Task<IActionResult> Index()
        {
            List<BankAccount> bankAccountList = new List<BankAccount>();

            //string requestURL = urlStub + bankAccountsEndPoint;
            //using (var httpClient = new HttpClient())
            //{
            //    using (var response = await httpClient.GetAsync(requestURL))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        BankAccountList = JsonConvert.DeserializeObject<List<BankAccount>>(apiResponse);
            //  }
            //}

            bankAccountList = await _financeRepository.GetBankAccountsAsync();

            return View(bankAccountList);

                //return View(await _context.BankAccount.ToListAsync());
        }

        // GET: BankAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccount = await _context.BankAccount
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AccountDescription,AccountType,AccountOwnerId,AccountBalance")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                var newAccount = await _financeRepository.AddBankAccountAsync(bankAccount);
                if (newAccount != null) 
                return RedirectToAction(nameof(Index));
            }
            return View(bankAccount);
        }

        // GET: BankAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bankAccount = new BankAccount();

            string requestURL = urlStub + bankAccountsEndPoint + "\\" + id;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(requestURL))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    bankAccount = JsonConvert.DeserializeObject<BankAccount>(apiResponse);
                }
            }

            if (bankAccount == null)
            {
                return NotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AccountDescription,AccountType,AccountOwnerId,AccountBalance")] BankAccount bankAccount)
        {
            if (id != bankAccount.ID)
            {
                return NotFound();
            }
            BankAccount updatedBankAccount = new BankAccount();


            string requestURL = urlStub + bankAccountsEndPoint + "/" + id;

             

            using (var httpClient = new HttpClient())
            {
               StringContent myBody = new StringContent(JsonConvert.SerializeObject(bankAccount), System.Text.Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(requestURL, myBody))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    updatedBankAccount = JsonConvert.DeserializeObject<BankAccount>(apiResponse);
                }
            }

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(bankAccount);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!BankAccountExists(bankAccount.ID))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            return View(updatedBankAccount != null ? updatedBankAccount : bankAccount);
        }

        // GET: BankAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccount = await _context.BankAccount
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            bool result = await _financeRepository.DeleteBankAccountAsync(id);

            //var bankAccount = await _context.BankAccount.FindAsync(id);
            //_context.BankAccount.Remove(bankAccount);
            //await _context.SaveChangesAsync();
            if (result) {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BankAccountExists(int id)
        {
            return _context.BankAccount.Any(e => e.ID == id);
        }
    }
}
