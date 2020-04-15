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
    public class GridController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public GridController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // GET: api/Grid/5
        [HttpGet("{idRes}")]
        public async Task<ActionResult<IEnumerable<RestaurantTables>>> GetTables(int idRes)
        {
            var restaurantTables = await _context.RestaurantTables.Include(r => r.Reservation).Where(r => r.Idrestaurant == idRes).ToListAsync();

            if (restaurantTables == null)
            {
                return NotFound();
            }

            return restaurantTables;
        }

        [HttpGet("TFloors/{idRes}/{idFloors}")]
        public async Task<ActionResult<IEnumerable<RestaurantTables>>> GetTablesByFloors(int idRes, int idFloors)
        {
            var restaurantTables = await _context.RestaurantTables.Include(r => r.Reservation).Where(r => r.Idrestaurant == idRes && r.FloorIndex == idFloors).ToListAsync();

            if (restaurantTables == null)
            {
                return NotFound();
            }

            return restaurantTables;
        }

        [HttpGet("Floors/{idRes}")]
        public async Task<ActionResult<Dictionary<int, string>>> GetFloors(int idRes)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            var floorIndexes = _context.RestaurantTables.Where(r => r.Idrestaurant == idRes).Select(f => f.FloorIndex).Distinct().ToList();
            

            if (floorIndexes == null)
            {
                return NotFound();
            }
            foreach(var i in floorIndexes)
            {
                string s = _context.RestaurantTables.Where(r => r.Idrestaurant == idRes && r.FloorIndex == i).Select(f => f.FloorName).First();
                dic.Add(i, s);
            }
            return dic;
        }

    }
}