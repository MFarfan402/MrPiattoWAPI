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
        // MAURICIO FARFAN
        // RestaurantView -> Reserva una mesa -> Buscar ahora -> Confirmar Datos
        // Method used to create a new reservation. It has the locks for the reservation. It retrieves if the reservations
        // are created in a correct way.
        [HttpPost]
        /*
         * ESTE ALGORITMO ESTÁ DÍFICIL DE EXPLICAR. POR LO QUE LO COMENTARÉ EN ESPAÑOL PARA NO MORIR A LA HORA DE DOCUMENTARLO:
         * 1. Verificamos que el usuario no tenga más de 3 reservaciones activas.
         * 2. Verificamos que el usuario no esté bloqueado por el restaurante.
         * 3. Verificamos que el restaurante no esté bloqueado ese dia.
         * 4. Buscamos las mesas correspondientes al restaurante que tengan la capacidad de sillas necesarias para el grupo de personas.
         *  4.1. Si encontramos mesas así: seleccionamos una mesa.
         *  4.2. Verificamos que la mesa no este bloqueada para la fecha y hora de la reservación.
         *  4.3. Buscamos todas las reservaciones que tiene esa mesa para ese día ordenadas de mayor a menor hora.
         *      (Si no tiene insertamos registro)
         *  4.4. Calculamos la diferencia con la reservación anterior que se tiene en esa mesa. Si es mayor a 2 horas insertamos registro.
         *  Si la mesa esta ocupada, regresamos al paso 4.1, hasta finalizar con todas las mesas. En caso de que no encuentre mesa es porque
         *  las mesas de esa dimensión no estaban disponibles para esa hora.
         *  
         *  5. Si no existen registros de mesas grandes es hora de pegar mesas.
         */
        public async Task<string> PostReservation(Reservation reservation)
        {
            // 1st step
            var userReservations = _context.Reservation.Where(r => r.Iduser == reservation.Iduser
            && r.Date > DateTime.Now).Count();

            if (userReservations >= 3)
                return "Ya tienes muchas reservaciones activas.";

            // 2nd step
            var lockedUser = _context.LockedRestaurants.Where(l => l.Iduser == reservation.Iduser
            && l.Idrestaurants == reservation.Idtable && l.UnlockedDate > DateTime.Now).Count();

            if (lockedUser != 0)
                return "Estas penalizado por el restaurante.";

            // 3rd step
            var lockedRes = _context.LockedHours.Where(l => l.Idrestaurant == reservation.Idtable
            && l.StartDate <= reservation.Date && l.EndDate >= reservation.Date).Count();

            if (lockedRes != 0)
                return "El restaurante no admite reservaciones para esa fecha.";

            // 4th step
            var tables = _context.RestaurantTables.Where(t => t.Idrestaurant == reservation.Idtable
            && t.Seats >= reservation.AmountOfPeople).ToList();
            
            List<Reservation> reservations;

            if (tables.Count() != 0)
            {
                // 4.1
                foreach (var table in tables)
                {
                    // 4.2
                    var lockedTable = _context.LockedTables.Where(t => t.Idtables == table.Idtables &&
                    t.StartDate <= reservation.Date && t.EndDate >= reservation.Date).Count();
                    if (lockedTable == 0)
                    {
                        // 4.3
                        reservations = _context.Reservation
                        .Where(r => r.Idtable == table.Idtables && r.Date.Date == reservation.Date.Date).OrderBy(r => r.Date)
                        .ToList();
                        if (reservations.Count == 0)
                        {
                            reservation.Idtable = table.Idtables;
                            _context.Reservation.Add(reservation);
                            await _context.SaveChangesAsync();
                            return "Tu reservación se ha generado con éxito.";
                        }
                        else
                        {
                            for(int i = reservations.Count - 1; i >= 0; i--)
                            {
                                if (reservation.Date > reservations[i].Date)
                                {
                                    if ((reservation.Date - reservations[i].Date).TotalMinutes > 119)
                                    {
                                        reservation.Idtable = table.Idtables;
                                        _context.Reservation.Add(reservation);
                                        await _context.SaveChangesAsync();
                                        return "Tu reservación se ha generado con éxito.";
                                    }
                                    else { i = -1; }
                                }
                                else
                                {
                                    if (i == 0)
                                    {
                                        if ((reservations[i].Date - reservation.Date).TotalMinutes > 119)
                                        {
                                            reservation.Idtable = table.Idtables;
                                            _context.Reservation.Add(reservation);
                                            await _context.SaveChangesAsync();
                                            return "Tu reservación se ha generado con éxito.";
                                        }
                                        else { i = 0; }
                                    }
                                }
                            }
                        }
                    }
                }
                return "No hay disponibilidad para este horario";
            }
            return "Favor de contactar al restaurante para hacer la reservación.";
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
