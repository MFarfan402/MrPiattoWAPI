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
            var restaurantTables = await _context.RestaurantTables.Where(r => r.Idrestaurant == idRes).ToListAsync();

            if (restaurantTables == null)
            {
                return NotFound();
            }

            return restaurantTables;
        }

    }
}