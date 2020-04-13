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
    public class ReservationsController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public ReservationsController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // GET: api/Reservations/{idUser}
        // MAURICIO FARFAN
        // Usuario -> Mis Reservaciones
        // Method used to retrieve the information of the future reservations of the client.
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservation(int id)
        {
            var reservations = await _context.Reservation
                .Where(r => r.Iduser == id && r.Date > DateTime.Now)
                .Include(res => res.IdtableNavigation).ThenInclude(r => r.IdrestaurantNavigation).ToListAsync();
            if (reservations == null)
                return NotFound();
            return reservations;
        }


        // PUT: api/Reservations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.Idreservation)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        // POST: api/Reservations
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Idreservation }, reservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Idreservation == id);
        }
    }
}
