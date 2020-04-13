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
    public class UserRestaurantsController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public UserRestaurantsController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // GET: api/UserRestaurants/{idUser}
        // MAURICIO FARFAN
        // Usuario -> Favoritos
        // Method used to retrieve the information of a restaurant marked as favorite from a user.
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetUserFavorites(int id)
        {
            var restaurants = await _context.UserRestaurant
                .Where(u => u.Iduser == id && u.Favorite == true)
                .Select(u => u.IdrestaurantNavigation)
                .Select(r => new Restaurant
                {
                    Idrestaurant = r.Idrestaurant,
                    Name = r.Name,
                    Address = r.Address,
                    Score = r.Score,
                    IdcategoriesNavigation = r.IdcategoriesNavigation
                })
                .ToListAsync();
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
