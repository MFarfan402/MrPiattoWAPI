using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrPiattoWAPI.Model;

namespace MrPiattoWAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public PoliciesController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // GET: api/Policies/{idRestaurant}
        // MAURICIO FARFAN
        // Usuario -> RestaurantView -> Reserva una mesa -> Políticas de cancelación.
        // Method used to get all the comments of a restaurant that are in an 'active' status.
        [HttpGet("{id}")]
        public async Task<ActionResult<Policies>> GetPolicies(int id)
        {
            var policies = await _context.Policies.Where(p => p.Idrestaurant == id).FirstAsync();

            if (policies == null)
                return NotFound();

            return policies;
        }

        // PUT: api/Policies/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPolicies(int id, Policies policies)
        {
            if (id != policies.Idpolicies)
            {
                return BadRequest();
            }

            _context.Entry(policies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoliciesExists(id))
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

        // POST: api/Policies
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Policies>> PostPolicies(Policies policies)
        {
            _context.Policies.Add(policies);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPolicies", new { id = policies.Idpolicies }, policies);
        }

        // DELETE: api/Policies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Policies>> DeletePolicies(int id)
        {
            var policies = await _context.Policies.FindAsync(id);
            if (policies == null)
            {
                return NotFound();
            }

            _context.Policies.Remove(policies);
            await _context.SaveChangesAsync();

            return policies;
        }

        private bool PoliciesExists(int id)
        {
            return _context.Policies.Any(e => e.Idpolicies == id);
        }
    }
}
