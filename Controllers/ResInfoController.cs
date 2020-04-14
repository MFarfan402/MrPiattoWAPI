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
    public class ResInfoController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public ResInfoController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // GET: api/ClientRes/5
        [HttpGet("{idRes}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int idRes)
        {
            var restaurant = await _context.Restaurant.Where(r => r.Idrestaurant == idRes).FirstAsync();

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }

        // GET: api/ResPolicies/5
        [HttpGet("Policies/{idRes}")]
        public async Task<ActionResult<Policies>> GetPolicies(int idRes)
        {
            var policies = await _context.Policies.Where(r => r.Idrestaurant == idRes).FirstAsync();

            if (policies == null)
            {
                return NotFound();
            }

            return policies;
        }
    }
}