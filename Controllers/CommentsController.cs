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
    [Route("api/Comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly MrPiattoDBContext _context;

        public CommentsController(MrPiattoDBContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        // Method used for the verifier to observe all of the bad comments that the users have reported.
        // Mauricio Farfán. Verificador -> Reseñas.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comments>>> GetBadComments()
        {
            return await _context.Comments.Where(c => c.Status == "Verifier").ToListAsync();
        }


        [HttpGet("newRoute")]
        public async Task<ActionResult<IEnumerable<Comments>>> GetGoodComments()
        {
            return await _context.Comments.Where(c => c.Status == "Active").ToListAsync();
        }


        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comments>> GetComments(int id)
        {
            var comments = await _context.Comments.FindAsync(id);

            if (comments == null)
            {
                return NotFound();
            }

            return comments;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComments(int id, Comments comments)
        {
            if (id != comments.Idcomments)
            {
                return BadRequest();
            }

            _context.Entry(comments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentsExists(id))
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

        // POST: api/Comments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Comments>> PostComments(Comments comments)
        {
            _context.Comments.Add(comments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComments", new { id = comments.Idcomments }, comments);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comments>> DeleteComments(int id)
        {
            var comments = await _context.Comments.FindAsync(id);
            if (comments == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comments);
            await _context.SaveChangesAsync();

            return comments;
        }

        private bool CommentsExists(int id)
        {
            return _context.Comments.Any(e => e.Idcomments == id);
        }
    }
}
