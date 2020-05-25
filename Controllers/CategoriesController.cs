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

        // GET: api/Categories/WaitersStatistics/{idRestaurant}
        // MAURICIO FARFAN
        // Lo pongo aqui porque no me dejo crear otro controladoooor. Big CHALE x2
        [HttpGet("WaitersStatistics/{id}/{rating}")]
        public async Task<bool> GetWaiters(int id, double rating)
        {
            WaiterStatistics waiter = new WaiterStatistics();
            waiter.Idwaiter = id;
            waiter.Rating = rating;
            waiter.DateStatistics = DateTime.Now;

            _context.WaiterStatistics.Add(waiter);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
