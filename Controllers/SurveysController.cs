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
    public class SurveysController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public SurveysController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // GET: api/Surveys/{idRestaurant}
        // MAURICIO FARFAN
        // Usuario -> RestaurantView && Usuario -> RestaurantView -> Ver todas las reseñas
        // Method used to get the score bars of a restaurant.
        [HttpGet("Bars/{id}")]
        public async Task<List<int>> GetScore(int id)
        {
            List<int> results = new List<int>();
            for (int i = 1; i <= 5; i++)
                results.Add(await _context.Surveys
                    .Where(s => s.Idrestaurant == id && s.GeneralScore >= i && s.GeneralScore < i + 1)
                    .CountAsync());
            var sum = results.Sum();
            if (sum != 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    results[i] *= 100;
                    results[i] /= sum;
                }
            }
            return results;
        }

        // GET: api/Surveys/{idRestaurant}
        // MAURICIO FARFAN
        // Usuario -> RestaurantView && Usuario -> RestaurantView -> Ver todas las reseñas
        // Method used to get the scores of a restaurant.
        [HttpGet("{id}")]
        public async Task<List<double>> GetSurveys(int id)
        {
            List<double> results = new List<double>();
            results.Add(await _context.Surveys.Where(s => s.Idrestaurant == id)
                .Select(s => s.FoodRating).AverageAsync());
            results.Add(await _context.Surveys.Where(s => s.Idrestaurant == id)
                .Select(s => s.ComfortRating).AverageAsync());
            results.Add(await _context.Surveys.Where(s => s.Idrestaurant == id)
                .Select(s => s.ServiceRating).AverageAsync());
            results.Add(await _context.Surveys.Where(s => s.Idrestaurant == id)
                .Select(s => s.GeneralScore).AverageAsync());

            return results;
        }

        // GET: api/Surveys/Comments/{idRestaurant}
        // MAURICIO FARFAN
        // Usuario -> RestaurantView -> Ver todas las reseñas
        // Method used to get the scores and the comments of a restaurant.
        [HttpGet("Comments/{id}")]
        public async Task<ActionResult<IEnumerable<Surveys>>> GetSurveysComments(int id)
        {
            return await _context.Surveys.Include(s => s.IdcommentNavigation)
                .Where(s => s.Idrestaurant == id && s.IdcommentNavigation.Status == "Aceptado")
                .ToListAsync();
        }

        // PUT: api/Surveys/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurveys(int id, Surveys surveys)
        {
            if (id != surveys.Idsurvey)
            {
                return BadRequest();
            }

            _context.Entry(surveys).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveysExists(id))
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

        // POST: api/Surveys
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Surveys>> PostSurveys(Surveys surveys)
        {
            _context.Surveys.Add(surveys);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurveys", new { id = surveys.Idsurvey }, surveys);
        }

        // DELETE: api/Surveys/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Surveys>> DeleteSurveys(int id)
        {
            var surveys = await _context.Surveys.FindAsync(id);
            if (surveys == null)
            {
                return NotFound();
            }

            _context.Surveys.Remove(surveys);
            await _context.SaveChangesAsync();

            return surveys;
        }

        private bool SurveysExists(int id)
        {
            return _context.Surveys.Any(e => e.Idsurvey == id);
        }
    }
}
