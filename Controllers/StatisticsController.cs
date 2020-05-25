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

        [HttpPost("Add/Day")]
        public async Task<string> AddDay(DayStatistics dayStats)
        {
            try
            {
                DayStatistics auxDay = new DayStatistics();

                auxDay.Idrestaurant = dayStats.Idrestaurant;
                auxDay.AverageMonday = dayStats.AverageMonday;
                auxDay.AverageTuesday = dayStats.AverageTuesday;
                auxDay.AverageWednesday = dayStats.AverageWednesday;
                auxDay.AverageThursday = dayStats.AverageThursday;
                auxDay.AverageFriday = dayStats.AverageFriday;
                auxDay.AverageSaturday = dayStats.AverageSaturday;
                auxDay.AverageSunday = dayStats.AverageSunday;
                auxDay.DateStatistics = dayStats.DateStatistics;

                _context.DayStatistics.Add(auxDay);
                await _context.SaveChangesAsync();

                return "Restaurante actualizado";

            }
            catch
            {
                return "Error. Hubo un error al actualizar";
            }
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

        [HttpPost("Add/Hour")]
        public async Task<string> AddHour(HourStatistics hourStats)
        {
            try
            {
                HourStatistics auxHour = new HourStatistics();

                auxHour.Idrestaurant = hourStats.Idrestaurant;
                auxHour.Average0000 = hourStats.Average0000;
                auxHour.Average0100 = hourStats.Average0100;
                auxHour.Average0200 = hourStats.Average0200;
                auxHour.Average0300 = hourStats.Average0300;
                auxHour.Average0400 = hourStats.Average0400;
                auxHour.Average0500 = hourStats.Average0500;
                auxHour.Average0600 = hourStats.Average0600;
                auxHour.Average0700 = hourStats.Average0700;
                auxHour.Average0800 = hourStats.Average0800;
                auxHour.Average0900 = hourStats.Average0900;
                auxHour.Average1000 = hourStats.Average1000;
                auxHour.Average1100 = hourStats.Average1100;
                auxHour.Average1200 = hourStats.Average1200;
                auxHour.Average1300 = hourStats.Average1300;
                auxHour.Average1400 = hourStats.Average1400;
                auxHour.Average1500 = hourStats.Average1500;
                auxHour.Average1600 = hourStats.Average1600;
                auxHour.Average1700 = hourStats.Average1700;
                auxHour.Average1800 = hourStats.Average1800;
                auxHour.Average1900 = hourStats.Average1900;
                auxHour.Average2000 = hourStats.Average2000;
                auxHour.Average2100 = hourStats.Average2100;
                auxHour.Average2200 = hourStats.Average2200;
                auxHour.Average2300 = hourStats.Average2300;
                auxHour.DateStatistics = hourStats.DateStatistics;

                _context.HourStatistics.Add(auxHour);
                await _context.SaveChangesAsync();

                return "Restaurante actualizado";

            }
            catch
            {
                return "Error. Hubo un error al actualizar";
            }
        }

        // GET: api/ClientRes/5
        [HttpGet("Table/{idRes}")]
        public async Task<ActionResult<IEnumerable<TableStatistics>>> GetTableStatistics(int idRes)
        {
            var statistics = await _context.TableStatistics.Where(r => r.IDRestaurant == idRes).Include(t => t.IdrestaurantTablesNavigation).ToListAsync();

            if (statistics == null)
            {
                return NotFound();
            }

            return statistics;
        }

        [HttpPost("Add/TableStats")]
        public async Task<string> AddTableStats(TableStatistics tableStats)
        {
            try
            {
                TableStatistics auxStats = new TableStatistics();

                auxStats.IDRestaurant = tableStats.IDRestaurant;
                auxStats.IDTable = tableStats.IDTable;
                auxStats.AvarageUse = tableStats.AvarageUse;
                auxStats.DateStatistics = tableStats.DateStatistics;
                
                _context.TableStatistics.Add(auxStats);
                await _context.SaveChangesAsync();

                return "Restaurante actualizado";

            }
            catch
            {
                return "Error. Hubo un error al actualizar";
            }
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