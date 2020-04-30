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

        // POST: api/ResInfo/
        // MAURICIO ANDRES
        // Method used to update restaurant data
        [HttpPost]
        public async Task<string> PostRestaurant(Restaurant restaurant)
        {
            try
            {
                var res = await _context.Restaurant
                    .Where(i => i.Idrestaurant == restaurant.Idrestaurant).FirstAsync();

                res.Mail = restaurant.Mail;
                res.Password = restaurant.Password;
                res.Confirmation = restaurant.Confirmation;
                res.LastLogin = restaurant.LastLogin;
                res.Name = restaurant.Name;
                res.Description = restaurant.Description;
                res.Address = restaurant.Address;
                res.Phone = restaurant.Phone;
                res.Dress = restaurant.Dress;
                res.Price = restaurant.Price;
                res.Score = restaurant.Score;
                res.SeverityLevel = restaurant.SeverityLevel;
                res.Long = restaurant.Long;
                res.Lat = restaurant.Lat;
                await _context.SaveChangesAsync();

                return "Restaurante actualizado";

            } catch
            {
                return "Error. No existe el restaurante";
            }
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

        // GET: api/Grid/5
        [HttpGet("Waiters/{idRes}")]
        public async Task<ActionResult<IEnumerable<Waiters>>> GetWaiters(int idRes)
        {
            var waiters = await _context.Waiters.Where(r => r.Idrestaurant == idRes).ToListAsync();

            if (waiters == null)
            {
                return NotFound();
            }

            return waiters;
        }
    }
}