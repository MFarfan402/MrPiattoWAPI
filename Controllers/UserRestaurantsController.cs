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
    [Route("api/UserRestaurants")]
    [ApiController]
    public class UserRestaurantsController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public UserRestaurantsController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // GET: api/UserRestaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRestaurant>>> GetUserRestaurant()
        {
            return await _context.UserRestaurant.ToListAsync();
        }

        // GET: api/UserRestaurants/5
        // Method used for the user to look for all of his restaurants marked as favorites.
        // Mauricio Farfán. Usuario -> Favoritos.
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetUserRestaurant(int id)
        {
            var restaurants = await _context.UserRestaurant
                .Where(u => u.Iduser == id && u.Favorite == true)
                .Select(u => u.IdrestaurantNavigation).Select(r => new Restaurant
                {
                    Idrestaurant = r.Idrestaurant,
                    Name = r.Name,
                    Address = r.Address,
                    Score = r.Score       
                }).ToListAsync();
            if (restaurants == null)
                return NotFound();

            return restaurants;
        }

        // PUT: api/UserRestaurants/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRestaurant(int id, UserRestaurant userRestaurant)
        {
            if (id != userRestaurant.IduserRestaurant)
            {
                return BadRequest();
            }

            _context.Entry(userRestaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRestaurantExists(id))
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

        // POST: api/UserRestaurants
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserRestaurant>> PostUserRestaurant(UserRestaurant userRestaurant)
        {
            _context.UserRestaurant.Add(userRestaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserRestaurant", new { id = userRestaurant.IduserRestaurant }, userRestaurant);
        }

        // DELETE: api/UserRestaurants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserRestaurant>> DeleteUserRestaurant(int id)
        {
            var userRestaurant = await _context.UserRestaurant.FindAsync(id);
            if (userRestaurant == null)
            {
                return NotFound();
            }

            _context.UserRestaurant.Remove(userRestaurant);
            await _context.SaveChangesAsync();

            return userRestaurant;
        }

        private bool UserRestaurantExists(int id)
        {
            return _context.UserRestaurant.Any(e => e.IduserRestaurant == id);
        }
    }
}
