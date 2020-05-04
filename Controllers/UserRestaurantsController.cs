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
            List<Restaurant> favorites = new List<Restaurant>();
            var restaurants = await _context.UserRestaurant
                .Where(u => u.Iduser == id && u.Favorite == true)
                .Select(id => new UserRestaurant
                { Idrestaurant = id.Idrestaurant })
                .ToListAsync();
            foreach (var r in restaurants)
            {
                favorites.Add(_context.Restaurant.Where(x => x.Idrestaurant == r.Idrestaurant)
                    .Include(y => y.IdcategoriesNavigation)
                    .FirstOrDefault());
            }

            if (restaurants == null)
                return NotFound();

            return favorites;
        }

        // GET: api/UserRestaurants/Visited/{idUser}
        // MAURICIO FARFAN
        // Menú lateral -> Visitados
        // Method used to retrieve the information of a restaurant that a user has visited.
        [HttpGet("Visited/{id}")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetUserVisited(int id)
        {
            List<Restaurant> visited = new List<Restaurant>();
            var restaurants = await _context.UserRestaurant
                .Where(u => u.Iduser == id && u.Visited == true)
                .Select(id => new UserRestaurant
                { Idrestaurant = id.Idrestaurant })
                .ToListAsync();
            foreach (var r in restaurants)
            {
                visited.Add(_context.Restaurant.Where(x => x.Idrestaurant == r.Idrestaurant)
                    .Include(y => y.IdcategoriesNavigation)
                    .FirstOrDefault());
            }

            if (restaurants == null)
                return NotFound();

            return visited;
        }

        // POST: api/userRestaurants
        // MAURICIO FARFAN
        // Usuario -> RestaruantView -> Hamburguer -> Añadir a favoritos
        // Method used to retrieve the information of a restaurant marked as favorite from a user.
        [HttpPost]
        public async Task<string> PostUserRestaurant(UserRestaurant userRestaurant)
        {
            var restaurant = await _context.UserRestaurant
                .Where(u => u.Iduser == userRestaurant.Iduser && u.Idrestaurant == userRestaurant.Idrestaurant).FirstAsync();

            if (restaurant == null)
            {
                _context.UserRestaurant.Add(userRestaurant);
                await _context.SaveChangesAsync();
            }
            else
            {
                restaurant.Favorite = true;
                _context.UserRestaurant.Update(restaurant);
                await _context.SaveChangesAsync();
            }
            return "Restaurante agregado a favoritos";
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

    }
}
