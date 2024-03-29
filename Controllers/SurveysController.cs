﻿using System;
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
        [HttpGet("Alexa/{idRes}/{A1}/{A2}/{A3}/{A4}/{A0}")]
        // idRestaurant, waiterCal, foodCal, comfortCal, generalCal, waiterName
        public async Task PostAlexaSurvey(int idRes, int A1, int A2, int A3, int A4, string A0)
        {
            Surveys survey = new Surveys();
            Waiters waiters;

            survey.Idrestaurant = idRes;
            survey.Iduser = 3; ///// ESTE DEBE DE SER EL USER GENERAL
            survey.FoodRating = A2 / 2;
            survey.ComfortRating = A3 / 2;
            survey.GeneralScore = A4 / 2;
            survey.ServiceRating = A1 / 2;
            survey.DateStatistics = DateTime.Now;
            if (A0 != "0")
            {
                waiters = await _context.Waiters.Where(w => w.Idrestaurant == idRes &&
                    w.WaiterFirstName.Contains(A0)).FirstOrDefaultAsync();
                survey.Idwaiter = (waiters == null) ? 0 : waiters.Idwaiter;
            }
            if (survey.Idwaiter == 0)
            {
                var genWaiter = await _context.Waiters.Where(w => w.Idrestaurant == idRes)
                    .OrderBy(w => w.Idwaiter).FirstOrDefaultAsync();
                survey.Idwaiter = genWaiter.Idwaiter;
            }
            _context.Surveys.Add(survey);
            await _context.SaveChangesAsync();
        }

        // GET: api/Surveys/{idRestaurant}
        // MAURICIO FARFAN
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
            if (_context.Surveys.Where(s => s.Idrestaurant == id).Count() > 0)
            {
                results.Add(await _context.Surveys.Where(s => s.Idrestaurant == id)
                .Select(s => s.FoodRating).AverageAsync());
                results.Add(await _context.Surveys.Where(s => s.Idrestaurant == id)
                    .Select(s => s.ComfortRating).AverageAsync());
                results.Add(await _context.Surveys.Where(s => s.Idrestaurant == id)
                    .Select(s => s.ServiceRating).AverageAsync());
                results.Add(await _context.Surveys.Where(s => s.Idrestaurant == id)
                    .Select(s => s.GeneralScore).AverageAsync());
            }
            else
            {
                for (int i = 0; i < 4; i++)
                    results.Add(5);
            }
            
            return results;
        }

        // GET: api/Surveys/Comments/{idRestaurant}
        // MAURICIO FARFAN
        // Usuario -> RestaurantView -> Ver todas las reseñas
        // Method used to get the scores and the comments of a restaurant.
        [HttpGet("Comments/{id}")]
        public async Task<ActionResult<IEnumerable<Surveys>>> GetSurveysComments(int id)
        {
            return await _context.Surveys
                .Include(s => s.IdcommentNavigation)
                .Where(s => s.Idrestaurant == id && s.IdcommentNavigation.Status == "Aceptado")
                .ToListAsync();
        }

        // POST: api/Surveys
        // MAURICIO FARFAN
        // Usuario -> Visitados -> Reseña -> Enviar datos
        // Method used to get the scores and the comments of a restaurant.
        [HttpPost]
        public async Task<string> PostSurveys(Surveys surveys)
        {
            _context.Surveys.Add(surveys);
            await _context.SaveChangesAsync();

            return "Su comentario se ha publicado correctamente";
        }
    }
}
