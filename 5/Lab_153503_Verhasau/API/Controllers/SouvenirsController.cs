using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using Domain.Entities;

namespace API.Controllers_
{
    [Route("api/[controller]")]
    [ApiController]
    public class SouvenirsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SouvenirsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Souvenirs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Souvenir>>> GetSouvenir()
        {
          if (_context.Souvenir == null)
          {
              return NotFound();
          }
            return await _context.Souvenir.ToListAsync();
        }

        // GET: api/Souvenirs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Souvenir>> GetSouvenir(int id)
        {
          if (_context.Souvenir == null)
          {
              return NotFound();
          }
            var souvenir = await _context.Souvenir.FindAsync(id);

            if (souvenir == null)
            {
                return NotFound();
            }

            return souvenir;
        }

        // PUT: api/Souvenirs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSouvenir(int id, Souvenir souvenir)
        {
            if (id != souvenir.Id)
            {
                return BadRequest();
            }

            _context.Entry(souvenir).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SouvenirExists(id))
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

        // POST: api/Souvenirs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Souvenir>> PostSouvenir(Souvenir souvenir)
        {
          if (_context.Souvenir == null)
          {
              return Problem("Entity set 'AppDbContext.Souvenir'  is null.");
          }
            _context.Souvenir.Add(souvenir);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSouvenir", new { id = souvenir.Id }, souvenir);
        }

        // DELETE: api/Souvenirs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSouvenir(int id)
        {
            if (_context.Souvenir == null)
            {
                return NotFound();
            }
            var souvenir = await _context.Souvenir.FindAsync(id);
            if (souvenir == null)
            {
                return NotFound();
            }

            _context.Souvenir.Remove(souvenir);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SouvenirExists(int id)
        {
            return (_context.Souvenir?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
