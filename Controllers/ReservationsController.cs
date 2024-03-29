﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using java.io;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrPiattoWAPI.Model;
using Newtonsoft.Json;
using RestSharp;

namespace MrPiattoWAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;
        readonly string pathQRBase = @"C:\Users\Administrador.WIN-IHAFOI0KOM7\Desktop\images\qr";
        readonly string pathServer = "http://200.23.157.109/images/qr/";

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

        // POST: api/Reservations/Update/
        // MAURICIO FARFAN
        [HttpPost("Update")]
        public async Task<string> ModifyReservation(Reservation reservation)
        {
            var restaurant = _context.RestaurantTables.Where(r => r.Idtables == reservation.Idtable).FirstOrDefault();

            var lockedRes = _context.LockedHours.Where(l => l.Idrestaurant == restaurant.Idrestaurant
            && l.StartDate <= reservation.Date && l.EndDate >= reservation.Date).Count();

            if (lockedRes != 0)
                return "El restaurante no admite reservaciones para esa fecha.";

            var tables = _context.RestaurantTables.Where(t => t.Idrestaurant == restaurant.Idrestaurant
            && t.Seats >= reservation.AmountOfPeople).ToList();

            List<Reservation> reservations;
            if (tables.Count() != 0)
            {
                foreach (var table in tables)
                {
                    var lockedTable = _context.LockedTables.Where(t => t.Idtables == table.Idtables &&
                    t.StartDate <= reservation.Date && t.EndDate >= reservation.Date).Count();
                    if (lockedTable == 0)
                    {
                        reservations = _context.Reservation
                        .Where(r => r.Idtable == table.Idtables && r.Date.Date == reservation.Date.Date).OrderBy(r => r.Date)
                        .ToList();
                        if (reservations.Count == 0)
                        {
                            reservation.Idtable = table.Idtables;
                            await UpdateReservationDBAsync(reservation);
                            return "¡Tu reservación ha sido actualizada con éxito! ¡Te esperamos!";
                        }
                        else
                        {
                            for (int i = reservations.Count - 1; i >= 0; i--)
                            {
                                if (reservation.Date > reservations[i].Date)
                                {
                                    if ((reservation.Date - reservations[i].Date).TotalMinutes > 119)
                                    {
                                        reservation.Idtable = table.Idtables;
                                        await UpdateReservationDBAsync(reservation);
                                        return "¡Tu reservación ha sido actualizada con éxito! ¡Te esperamos!";
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
                                            await UpdateReservationDBAsync(reservation);
                                            return "¡Tu reservación ha sido actualizada con éxito! ¡Te esperamos!";
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

        private async Task UpdateReservationDBAsync(Reservation reservation)
        {
            var oldRes = _context.Reservation.Where(r => r.Idreservation == reservation.Idreservation).FirstOrDefault();
            oldRes.Date = reservation.Date;
            oldRes.AmountOfPeople = reservation.AmountOfPeople;
            oldRes.Idtable = reservation.Idtable;

            _context.Reservation.Update(oldRes);
            await _context.SaveChangesAsync();
        }

        // GET: api/Reservations/Delete/{id}
        // MAURICIO FARFAN
        // Usuario -> Mis Reservaciones
        // Method used to retrieve the information of the future reservations of the client.
        [HttpGet("Delete/{id}")]
        public async Task<bool> DeleteReservationUser(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            CheckForStrike(reservation);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }

        private void CheckForStrike(Reservation reservation)
        {
        }

        [HttpGet("QR/{id}")]
        public async Task<bool> FireAndForgetQr(int id)
        {
            var reservation = _context.Reservation.Where(r => r.Iduser == id && r.Url == ".").FirstOrDefault();
            if (reservation == null)
                return false;
            return await GenerateQRAsync(reservation);
             
        }
        // GET: api/Reservations/{idUser}
        // MAURICIO ANDRES
        // Restaurante -> Mis Reservaciones
        // Method used to retrieve the information of the future reservations of the restaurant.
        [HttpGet("Res/{id}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationRes(int id)
        {
            var reservations = await _context.Reservation
                .Where(r => r.IdtableNavigation.IdrestaurantNavigation.Idrestaurant == id && r.Date > DateTime.Now)
                .Include(u => u.IduserNavigation)
                .Include(t => t.IdtableNavigation).ToListAsync();
            if (reservations == null)
                return NotFound();
            return reservations;
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
        public async Task<string> PostReservationAsync(Reservation reservation)
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

                            return 1.ToString();
                        }
                        else
                        {
                            for (int i = reservations.Count - 1; i >= 0; i--)
                            {
                                if (reservation.Date > reservations[i].Date)
                                {
                                    if ((reservation.Date - reservations[i].Date).TotalMinutes > 119)
                                    {
                                        reservation.Idtable = table.Idtables;
                                        _context.Reservation.Add(reservation);
                                        await _context.SaveChangesAsync();
                                        return 1.ToString();
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
                                            return 1.ToString();
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

        // GET: api/Reservations/{idUser}
        // MAURICIO ANDRES
        // Notificaciones
        // Method used to retrieve the information of the future reservations of the restaurant.
        [HttpGet("Not/Res/{id}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetResNotifications(int id)
        {
            var reservations = await _context.Reservation
                .Where(r => r.IdtableNavigation.IdrestaurantNavigation.Idrestaurant == id 
                && DateTime.Now.AddMinutes(30) >= r.Date && r.Date >= DateTime.Now)
                .Include(u => u.IduserNavigation)
                .Include(t => t.IdtableNavigation).ToListAsync();
            if (reservations == null)
                return NotFound();
            return reservations;
        }

        // GET: api/Reservations/{idUser}
        // MAURICIO ANDRES
        // Notificaciones
        // Method used to retrieve the information of the future reservations of the restaurant.
        [HttpPost("Not/Res/Update")]
        public async Task<string> UpdateResNotifications(Reservation reservation)
        {
            try
            {
                var res = await _context.Reservation.Where(r => r.Idreservation == reservation.Idreservation).FirstAsync();

                res.Checked = reservation.Checked;

                await _context.SaveChangesAsync();
                return "Base de datos actualizada";
            } catch
            {
                return "Error. Hubo un error al actualizar la base de datos";
            }
        }

        // GET: api/Reservations/{idUser}
        // MAURICIO ANDRES
        // Notificaciones
        // Method used to retrieve the information of the future reservations of the restaurant.
        [HttpPost("Not/Res/UpdateFromNot")]
        public async Task<string> UpdateResNotifications2(Reservation reservation)
        {
            try
            {
                var res = await _context.Reservation.Where(r => r.Idreservation == reservation.Idreservation).FirstAsync();

                res.CheckedFromApp = reservation.CheckedFromApp;

                await _context.SaveChangesAsync();
                return "Base de datos actualizada";
            }
            catch
            {
                return "Error. Hubo un error al actualizar la base de datos";
            }
        }

        // GET: api/Reservations/{idUser}
        // MAURICIO ANDRES
        // Notificaciones
        // Method used to retrieve the information of the future reservations of the restaurant.
        [HttpGet("Not/Res/{id}/{date}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetUserReservationsByHour(int id, DateTime date)
        {
            var reservations = await _context.Reservation
                .Where(r => r.IdtableNavigation.IdrestaurantNavigation.Idrestaurant == id
                && r.Date == date)
                .Include(u => u.IduserNavigation)
                .Include(t => t.IdtableNavigation).ToListAsync();
            if (reservations == null)
                return NotFound();
            return reservations;
        }

        // GET: api/Reservations/{idUser}
        // MAURICIO ANDRES
        // Notificaciones
        // Method used to retrieve the information of the future reservations of the restaurant.
        [HttpGet("Not/Aux/{id}")]
        public async Task<ActionResult<IEnumerable<AuxiliarReservation>>> GetAuxNotifications(int id)
        {
            var reservations = await _context.AuxiliarReservation
                .Where(r => r.IdauxiliarTableNavigation.Idrestaurant == id
                && DateTime.Now.AddMinutes(30) >= r.Date && r.Date >= DateTime.Now)
                .Include(t => t.IdauxiliarTableNavigation).ToListAsync();
            if (reservations == null)
                return NotFound();
            return reservations;
        }

        // GET: api/Reservations/{idUser}
        // MAURICIO ANDRES
        // Notificaciones
        // Method used to retrieve the information of the future reservations of the restaurant.
        [HttpPost("Not/Aux/Update")]
        public async Task<string> UpdateAuxNotifications(AuxiliarReservation reservation)
        {
            try
            {
                var res = await _context.AuxiliarReservation.Where(r => r.IdauxiliarReservation == reservation.IdauxiliarReservation).FirstAsync();

                res.Checked = reservation.Checked;

                await _context.SaveChangesAsync();
                return "Base de datos actualizada";
            }
            catch
            {
                return "Error. Hubo un error al actualizar la base de datos";
            }
        }

        // GET: api/Reservations/{idUser}
        // MAURICIO ANDRES
        // Notificaciones
        // Method used to retrieve the information of the future reservations of the restaurant.
        [HttpPost("Not/Aux/UpdateFromNot")]
        public async Task<string> UpdateAuxNotifications2(AuxiliarReservation reservation)
        {
            try
            {
                var res = await _context.AuxiliarReservation.Where(r => r.IdauxiliarReservation == reservation.IdauxiliarReservation).FirstAsync();

                res.CheckedFromApp = reservation.CheckedFromApp;

                await _context.SaveChangesAsync();
                return "Base de datos actualizada";
            }
            catch
            {
                return "Error. Hubo un error al actualizar la base de datos";
            }
        }

        // GET: api/Reservations/{idUser}
        // MAURICIO ANDRES
        // Notificaciones
        // Method used to retrieve the information of the future reservations of the restaurant.
        [HttpGet("Not/Aux/All/{id}")]
        public async Task<ActionResult<IEnumerable<AuxiliarReservation>>> GetAllAuxNotifications(int id)
        {
            var reservations = await _context.AuxiliarReservation
                .Where(r => r.IdauxiliarTableNavigation.Idrestaurant == id)
                .Include(t => t.IdauxiliarTableNavigation).ToListAsync();
            if (reservations == null)
                return NotFound();
            return reservations;
        }

        // GET: api/Reservations/{idUser}
        // MAURICIO ANDRES
        // Notificaciones
        // Method used to retrieve the information of the future reservations of the restaurant.
        [HttpGet("Not/Aux/{id}/{date}")]
        public async Task<ActionResult<IEnumerable<AuxiliarReservation>>> GetAuxReservationsByHour(int id, DateTime date)
        {
            var reservations = await _context.AuxiliarReservation
                .Where(r => r.IdauxiliarTableNavigation.Idrestaurant == id
                && r.Date.Year == date.Year
                && r.Date.Month == date.Month
                && r.Date.Day == date.Day
                && r.Date.Hour == date.Hour
                && r.Date.Minute == date.Minute)
                .Include(t => t.IdauxiliarTableNavigation).ToListAsync();
            if (reservations == null)
                return NotFound();
            return reservations;
        }

        // GET: api/Reservations/{idUser}
        // MAURICIO ANDRES
        // Notificaciones
        // Method used to retrieve the information of the future reservations of the restaurant.
        [HttpPost("Not/Aux")]
        public async Task<string> AddManReservation(AuxiliarReservation reservation)
        {
            try
            {
                AuxiliarReservation aux = new AuxiliarReservation();

                aux.IdauxiliarTable = reservation.IdauxiliarTable;
                aux.Date = reservation.Date;
                aux.AmountOfPeople = reservation.AmountOfPeople;
                aux.ReservationStatus = reservation.ReservationStatus;
                aux.Url = reservation.Url;
                aux.Phone = reservation.Phone;
                aux.Checked = reservation.Checked;
                aux.Name = reservation.Name;
                aux.LastName = reservation.LastName;

                _context.AuxiliarReservation.Add(aux);
                await _context.SaveChangesAsync();
                return "Reservacion agregada";
            } catch
            {
                return "Error. Hubo un error al actualizar la base de datos";
            }
 
        }

        // GET: api/Reservations/{idUser}
        // MAURICIO ANDRES
        // Notificaciones
        // Method used to retrieve the information of the future reservations of the restaurant.
        [HttpGet("Not/ManRes/{id}")]
        public async Task<ActionResult<IEnumerable<ManualReservations>>> GetManResNotifications(int id)
        {
            var reservations = await _context.ManualReservations
                .Where(r => r.IdtableNavigation.Idrestaurant == id
                && DateTime.Now.AddMinutes(30) >= r.Date && r.Date >= DateTime.Now)
                .Include(t => t.IdtableNavigation).ToListAsync();
            if (reservations == null)
                return NotFound();
            return reservations;
        }

        // GET: api/Reservations/{idUser}
        // MAURICIO ANDRES
        // Notificaciones
        // Method used to retrieve the information of the future reservations of the restaurant.
        [HttpGet("Not/ManRes/{id}/{date}")]
        public async Task<ActionResult<IEnumerable<ManualReservations>>> GetManReservationsByHour(int id, DateTime date)
        {
            var reservations = await _context.ManualReservations
                .Where(r => r.IdtableNavigation.Idrestaurant == id
                && r.Date.Year == date.Year 
                && r.Date.Month == date.Month
                && r.Date.Day == date.Day
                && r.Date.Hour == date.Hour
                && r.Date.Minute == date.Minute)
                .Include(t => t.IdtableNavigation).ToListAsync();
            if (reservations == null)
                return NotFound();
            return reservations;
        }

        // GET: api/Reservations/{idUser}
        // MAURICIO ANDRES
        // Notificaciones
        // Method used to retrieve the information of the future reservations of the restaurant.
        [HttpPost("Not/ManRes")]
        public async Task<string> AddManReservation(ManualReservations reservation)
        {
            try
            {
                ManualReservations manual = new ManualReservations();

                manual.IDTable = reservation.IDTable;
                manual.Date = reservation.Date;
                manual.AmountOfPeople = reservation.AmountOfPeople;
                manual.Checked = reservation.Checked;
                manual.Name = reservation.Name;
                manual.LastName = reservation.LastName;

                _context.ManualReservations.Add(manual);
                await _context.SaveChangesAsync();
                return "Reservacion agregada";
            } catch
            {
                return "Error. Hubo un error al actualizar la base de datos";
            }
        }

        // GET: api/Reservations/{idUser}
        // MAURICIO ANDRES
        // Notificaciones
        // Method used to retrieve the information of the future reservations of the restaurant.
        [HttpPost("Not/ManRes/Update")]
        public async Task<string> UpdateManNotifications(ManualReservations reservation)
        {
            try
            {
                var res = await _context.ManualReservations.Where(r => r.IDReservation == reservation.IDReservation).FirstAsync();

                res.Checked = reservation.Checked;

                await _context.SaveChangesAsync();
                return "Base de datos actualizada";
            }
            catch
            {
                return "Error. Hubo un error al actualizar la base de datos";
            }
        }

        // GET: api/Reservations/{idUser}
        // MAURICIO ANDRES
        // Notificaciones
        // Method used to retrieve the information of the future reservations of the restaurant.
        [HttpPost("Not/ManRes/UpdateFromNot")]
        public async Task<string> UpdateManNotifications2(ManualReservations reservation)
        {
            try
            {
                var res = await _context.ManualReservations.Where(r => r.IDReservation == reservation.IDReservation).FirstAsync();

                res.CheckedFromApp = reservation.CheckedFromApp;

                await _context.SaveChangesAsync();
                return "Base de datos actualizada";
            }
            catch
            {
                return "Error. Hubo un error al actualizar la base de datos";
            }
        }

        private async Task<bool> GenerateQRAsync(Reservation reservation)
        {
            
            var client = new RestClient($"https://qrcode3.p.rapidapi.com/generateQR?fill_style=solid&inner_eye_style=circle&style=circle&outer_eye_style=circle&ec_level=M&format=png&size=800&text={reservation.Idreservation}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "qrcode3.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "2226ad8917msh0d8f7e8e40c4b9fp19d487jsn5d51681b32fc");
            IRestResponse response = client.Execute(request);

            byte[] bitmap = response.RawBytes;

            Image image = Image.FromStream(new MemoryStream(bitmap));
            image.Save($@"{pathQRBase}\{reservation.Iduser}_{reservation.Idreservation}.png", ImageFormat.Png);

            reservation.Url = $"{pathServer}{reservation.Iduser}_{reservation.Idreservation}.png";
            _context.Entry(reservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
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

        [HttpGet("Reservation/All/{id}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAllReservations(int id)
        {
            var reservations = await _context.Reservation.Include(t => t.IdtableNavigation).Include(r => r.IduserNavigation).Where(r => r.IdtableNavigation.Idrestaurant == id).ToListAsync();
            if (reservations == null)
                return NotFound();
            return reservations;
        }

        [HttpGet("ManualReservation/All/{id}")]
        public async Task<ActionResult<IEnumerable<ManualReservations>>> GetAllManualReservations(int id)
        {
            var reservations = await _context.ManualReservations.Include(t => t.IdtableNavigation).Where(r => r.IdtableNavigation.Idrestaurant == id).ToListAsync();
            if (reservations == null)
                return NotFound();
            return reservations;
        }

        [HttpGet("AuxiliarReservation/All/{id}")]
        public async Task<ActionResult<IEnumerable<AuxiliarReservation>>> GetAllAuxiliarReservations(int id)
        {
            var reservations = await _context.AuxiliarReservation.Include(t => t.IdauxiliarTableNavigation).Where(r => r.IdauxiliarTableNavigation.Idrestaurant == id).ToListAsync();
            if (reservations == null)
                return NotFound();
            return reservations;
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Idreservation == id);
        }
    }
}
