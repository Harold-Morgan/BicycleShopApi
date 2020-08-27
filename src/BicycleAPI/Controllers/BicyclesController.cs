using BicycleService.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BicyclesController : ControllerBase
    {
        private readonly ShopContext _context;

        public BicyclesController(ShopContext context)
        {
            _context = context;
        }

        // GET: api/Bicycles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bicycle>>> GetBicycles()
        {
            return await _context.Bicycles.ToListAsync();
        }

        // GET: api/Bicycles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bicycle>> GetBicycle(int id)
        {
            var bicycle = await _context.Bicycles.FindAsync(id);

            if (bicycle == null)
            {
                return NotFound();
            }

            return bicycle;
        }

        // PUT: api/Bicycles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBicycle(int id, Bicycle bicycle)
        {
            if (id != bicycle.Id)
            {
                return BadRequest();
            }

            _context.Entry(bicycle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BicycleExists(id))
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

        // POST: api/Bicycles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Bicycle>> PostBicycle(Bicycle bicycle)
        {
            _context.Bicycles.Add(bicycle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBicycle", new { id = bicycle.Id }, bicycle);
        }

        // DELETE: api/Bicycles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bicycle>> DeleteBicycle(int id)
        {
            var bicycle = await _context.Bicycles.FindAsync(id);
            if (bicycle == null)
            {
                return NotFound();
            }

            _context.Bicycles.Remove(bicycle);
            await _context.SaveChangesAsync();

            return bicycle;
        }

        private bool BicycleExists(int id)
        {
            return _context.Bicycles.Any(e => e.Id == id);
        }
    }
}
