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
    public class UserStrikesController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public UserStrikesController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // GET: api/UserStrikes/5
        // MAURICIO FARFAN
        // Menu lateral -> Strikes
        // Used to get all the strikes of a certain user.
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<UserStrikes>>> GetUserStrikes(int id)
        {
            var userStrikes = await _context.UserStrikes.Where(s => s.Iduser == id).ToListAsync();

            if (userStrikes == null)
            {
                return NotFound();
            }

            return userStrikes;
        }

        // PUT: api/UserStrikes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserStrikes(int id, UserStrikes userStrikes)
        {
            if (id != userStrikes.IduserStrikes)
            {
                return BadRequest();
            }

            _context.Entry(userStrikes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserStrikesExists(id))
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

        // POST: api/UserStrikes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserStrikes>> PostUserStrikes(UserStrikes userStrikes)
        {
            _context.UserStrikes.Add(userStrikes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserStrikes", new { id = userStrikes.IduserStrikes }, userStrikes);
        }

        // DELETE: api/UserStrikes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserStrikes>> DeleteUserStrikes(int id)
        {
            var userStrikes = await _context.UserStrikes.FindAsync(id);
            if (userStrikes == null)
            {
                return NotFound();
            }

            _context.UserStrikes.Remove(userStrikes);
            await _context.SaveChangesAsync();

            return userStrikes;
        }

        private bool UserStrikesExists(int id)
        {
            return _context.UserStrikes.Any(e => e.IduserStrikes == id);
        }
    }
}
