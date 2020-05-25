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
    public class CategoriesController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public CategoriesController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // GET: api/Categories
        // MAURICIO FARFAN
        // Usuario -> Inicio
        // Method used to get all the categories with restaurants available
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> GetCategories()
        {
            List<Categories> categories = await _context.Categories.ToListAsync();
            List<Categories> finalCat = new List<Categories>();
            Random random = new Random();
            for(int i = 0; i < 4;)
            {
                int index = random.Next(categories.Count());
                if(!finalCat.Contains(categories[index]))
                {
                    finalCat.Add(categories[index]);
                    i++;
                }
            }
            return finalCat;
        }

        // GET: api/Categories/Waiters/{idRestaurant}
        // MAURICIO FARFAN
        // Lo pongo aqui porque no me dejo crear otro controladoooor. Big CHALE
        [HttpGet("Waiters/{id}")]
        public async Task<ActionResult<IEnumerable<Waiters>>> GetWaiters(int id)
        {
            var waiters = await _context.Waiters.Where(w => w.Idrestaurant == id).ToListAsync();
            if (waiters == null)
                return null;
            return waiters;
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categories>> GetCategories(int id)
        {
            var categories = await _context.Categories.FindAsync(id);

            if (categories == null)
            {
                return NotFound();
            }

            return categories;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategories(int id, Categories categories)
        {
            if (id != categories.Idcategory)
            {
                return BadRequest();
            }

            _context.Entry(categories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriesExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Categories>> PostCategories(Categories categories)
        {
            _context.Categories.Add(categories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategories", new { id = categories.Idcategory }, categories);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categories>> DeleteCategories(int id)
        {
            var categories = await _context.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categories);
            await _context.SaveChangesAsync();

            return categories;
        }

        private bool CategoriesExists(int id)
        {
            return _context.Categories.Any(e => e.Idcategory == id);
        }
    }
}
