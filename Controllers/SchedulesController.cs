using System;
using System.Collections.Generic;
using System.IO;
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
    public class SchedulesController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public SchedulesController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // GET: api/Schedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedule()
        {
            return await _context.Schedule.ToListAsync();
        }

        // GET: api/Schedules/{idRestaurant}
        // MAURICIO FARFAN
        // Usuario -> RestaurantView
        // Method used to get the schedule of a restaurant.
        [HttpGet("{id}")]
        public async Task<string> GetSchedule(int id)
        {
            var schedule = await _context.Schedule.Where(s => s.Idrestaurant == id).FirstAsync();

            if (schedule == null)
                return NotFound().ToString();

            return schedule.ToString();
        }

        // MAURICIO FLORES
        // Method used to get the schedule of a restaurant in json format
        [HttpGet("Raw/{id}")]
        public async Task<ActionResult<Schedule>> GetScheduleRaw(int id)
        {
            var schedule = await _context.Schedule.Where(s => s.Idrestaurant == id).FirstAsync();

            if (schedule == null)
                return NotFound();

            return schedule;
        }

        // PUT: api/Schedules/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, Schedule schedule)
        {
            if (id != schedule.Idschedule)
            {
                return BadRequest();
            }

            _context.Entry(schedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(id))
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

        // POST: api/Schedules
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Schedule>> PostSchedule(Schedule schedule)
        {
            _context.Schedule.Add(schedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchedule", new { id = schedule.Idschedule }, schedule);
        }

        // POST: api/ResInfo/
        // MAURICIO ANDRES
        // Method used to update restaurant data
        [HttpPost("Update")]
        public async Task<string> PostUpdateSchedule(Schedule schedule)
        {
            try
            {
                var res = await _context.Schedule
                    .Where(i => i.Idschedule == schedule.Idschedule).FirstAsync();

                res.Otmonday = schedule.Otmonday;
                res.Ctmonday = schedule.Ctmonday;
                res.Ottuesday = schedule.Ottuesday;
                res.Cttuestday = schedule.Cttuestday;
                res.Otwednesday = schedule.Otwednesday;
                res.Ctwednesday = schedule.Ctwednesday;
                res.Otthursday = schedule.Otthursday;
                res.Ctthursday = schedule.Ctthursday;
                res.Otfriday = schedule.Otfriday;
                res.Ctfriday = schedule.Ctfriday;
                res.Otsaturday = schedule.Otsaturday;
                res.Ctsaturday = schedule.Ctsaturday;
                res.Otsunday = schedule.Otsunday;
                res.Ctsunday = schedule.Ctsunday;
                await _context.SaveChangesAsync();

                return "Horario actualizado";

            }
            catch
            {
                return "Error. No existe el horario en el restuarante";
            }
        }

        // DELETE: api/Schedules/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Schedule>> DeleteSchedule(int id)
        {
            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _context.Schedule.Remove(schedule);
            await _context.SaveChangesAsync();

            return schedule;
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedule.Any(e => e.Idschedule == id);
        }
    }
}
