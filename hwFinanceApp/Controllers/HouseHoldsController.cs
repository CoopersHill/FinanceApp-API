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
    public class HouseHoldsController : ControllerBase
    {
        private readonly FinanceContext _context;

        public HouseHoldsController(FinanceContext context)
        {
            _context = context;
        }

        // GET: api/HouseHolds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HouseHold>>> GetHouseHolds()
        {
            return await _context.HouseHolds.ToListAsync();
        }

        // GET: api/HouseHolds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HouseHold>> GetHouseHold(int id)
        {
            var houseHold = await _context.HouseHolds.FindAsync(id);

            if (houseHold == null)
            {
                return NotFound();
            }

            return houseHold;
        }

        // PUT: api/HouseHolds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHouseHold(int id, HouseHold houseHold)
        {
            if (id != houseHold.Id)
            {
                return BadRequest();
            }

            _context.Entry(houseHold).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HouseHoldExists(id))
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

        // POST: api/HouseHolds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HouseHold>> PostHouseHold(HouseHold houseHold)
        {
            _context.HouseHolds.Add(houseHold);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHouseHold", new { id = houseHold.Id }, houseHold);
        }

        // DELETE: api/HouseHolds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHouseHold(int id)
        {
            var houseHold = await _context.HouseHolds.FindAsync(id);
            if (houseHold == null)
            {
                return NotFound();
            }

            _context.HouseHolds.Remove(houseHold);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HouseHoldExists(int id)
        {
            return _context.HouseHolds.Any(e => e.Id == id);
        }
    }
}
