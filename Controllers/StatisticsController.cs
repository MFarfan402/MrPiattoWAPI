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
        public async Task<ActionResult<DayStatistics>> GetDayStatistics(int idRes)
        {
            var statistics = await _context.DayStatistics.Where(r => r.Idrestaurant == idRes).FirstAsync();

            if (statistics == null)
            {
                return NotFound();
            }

            return statistics;
        }

        // GET: api/ClientRes/5
        [HttpGet("Hour/{idRes}")]
        public async Task<ActionResult<HourStatistics>> GetHourStatistics(int idRes)
        {
            var statistics = await _context.HourStatistics.Where(r => r.Idrestaurant == idRes).FirstAsync();

            if (statistics == null)
            {
                return NotFound();
            }

            return statistics;
        }
    }
}