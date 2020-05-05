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
    public class RestaurantsController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public RestaurantsController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // GET: api/Restaurants
        // MAURICIO FARFAN
        // Verficador -> Añadir
        // Method used to get all the possibles restaurants to MrPiattoService.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurant()
        {
            return await _context.Restaurant.Where(r => r.Confirmation == false).ToListAsync();
        }

        // GET: api/Restaurants/{idRestaurant}
        // MAURICIO FARFAN
        // Usuario -> RestaurantView
        // Method used to get all the information about a restaurant.
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _context.Restaurant.Where(r => r.Idrestaurant == id)
                .Include(r => r.IdcategoriesNavigation)
                .Include(r => r.IdpaymentNavigation)
                .FirstAsync();

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }

        [HttpGet("MainPage")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetUserMainPage()
        {
            Random random = new Random();
            var restaurants = await _context.Restaurant.Where(r => r.Confirmation == true)
                .Include(c=>c.IdcategoriesNavigation).Include(z => z.IdpaymentNavigation).ToListAsync();
            List<Restaurant> resOnScreen = new List<Restaurant>();
            for (int i = 0; i < 4;)
            {
                int index = random.Next(restaurants.Count);
                if (!resOnScreen.Contains(restaurants[index]))
                {
                    resOnScreen.Add(restaurants[index]);
                    i++;
                }
            }
            return resOnScreen;
            
        }
    }
}
