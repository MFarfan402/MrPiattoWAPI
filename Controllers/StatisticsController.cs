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
    public class StatisticsController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public StatisticsController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // GET: api/ClientRes/5
        [HttpGet("Day/{idRes}")]
        public async Task<ActionResult<IEnumerable<DayStatistics>>> GetDayStatistics(int idRes)
        {
            var statistics = await _context.DayStatistics.Where(r => r.Idrestaurant == idRes).ToListAsync();

            if (statistics == null)
            {
                return NotFound();
            }

            return statistics;
        }

        // GET: api/ClientRes/5
        [HttpGet("Hour/{idRes}")]
        public async Task<ActionResult<IEnumerable<HourStatistics>>> GetHourStatistics(int idRes)
        {
            var statistics = await _context.HourStatistics.Where(r => r.Idrestaurant == idRes).ToListAsync();

            if (statistics == null)
            {
                return NotFound();
            }

            return statistics;
        }

        // GET: api/ClientRes/5
        [HttpGet("Table/{idRes}")]
        public async Task<ActionResult<IEnumerable<TableStatistics>>> GetTableStatistics(int idRes)
        {
            var statistics = await _context.TableStatistics.Where(r => r.IDRestaurant == idRes).ToListAsync();

            if (statistics == null)
            {
                return NotFound();
            }

            return statistics;
        }

        // GET: api/Grid/5
        [HttpGet("Waiters/{idRes}")]
        public async Task<ActionResult<IEnumerable<WaiterStatistics>>> GetWaitersStats(int idRes)
        {
            var waiters = await _context.WaiterStatistics.Where(r => r.IdwaitersNavigation.Idrestaurant == idRes).ToListAsync();

            if (waiters == null)
            {
                return NotFound();
            }

            return waiters;
        }

        // GET: api/ClientRes/5
        [HttpGet("Survey/{idRes}")]
        public async Task<ActionResult<IEnumerable<Surveys>>> GetSurveys(int idRes)
        {
            var surveys = await _context.Surveys.Where(r => r.Idrestaurant == idRes).ToListAsync();

            if (surveys == null)
            {
                return NotFound();
            }

            return surveys;
        }
    }
}