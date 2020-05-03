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
    public class GridController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public GridController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // GET: api/Grid/5
        [HttpGet("{idRes}")]
        public async Task<ActionResult<IEnumerable<RestaurantTables>>> GetTables(int idRes)
        {
            var restaurantTables = await _context.RestaurantTables.Include(r => r.Reservation).Where(r => r.Idrestaurant == idRes).ToListAsync();

            if (restaurantTables == null)
            {
                return NotFound();
            }

            return restaurantTables;
        }

        [HttpGet("TFloors/{idRes}/{idFloors}")]
        public async Task<ActionResult<IEnumerable<RestaurantTables>>> GetTablesByFloors(int idRes, int idFloors)
        {
            var restaurantTables = await _context.RestaurantTables.Include(r => r.Reservation).Where(r => r.Idrestaurant == idRes && r.FloorIndex == idFloors).ToListAsync();

            if (restaurantTables == null)
            {
                return NotFound();
            }

            return restaurantTables;
        }

        [HttpGet("Floors/{idRes}")]
        public async Task<ActionResult<Dictionary<int, string>>> GetFloors(int idRes)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            var floorIndexes = await _context.RestaurantTables.Where(r => r.Idrestaurant == idRes).Select(f => f.FloorIndex).Distinct().ToListAsync();
            

            if (floorIndexes == null)
            {
                return NotFound();
            }
            foreach(var i in floorIndexes)
            {
                string s = await _context.RestaurantTables.Where(r => r.Idrestaurant == idRes && r.FloorIndex == i).Select(f => f.FloorName).FirstAsync();
                dic.Add(i, s);
            }
            return dic;
        }

        // POST: api/ResInfo/
        // MAURICIO ANDRES
        // Method used to add a table
        [HttpPost]
        public async Task<string> AddRestaurant(RestaurantTables restaurant)
        {
            try
            {
                _context.RestaurantTables.Add(restaurant);
                await _context.SaveChangesAsync();
                return "Base de datos actualizada";
            }
            catch
            {
                return "Error. Hubo un error al actualizar la base de datos";
            }
        }

        // POST: api/ResInfo/
        // MAURICIO ANDRES
        // Method used to add a table
        [HttpPost("UpdateTable")]
        public async Task<string> UpdateRestaurant(RestaurantTables restaurant)
        {
            try
            {
                var res = await _context.RestaurantTables
                    .Where(i => i.Idtables == restaurant.Idtables).FirstAsync();

                res.CoordenateX = restaurant.CoordenateX;
                res.CoordenateY = restaurant.CoordenateY;
                res.Seats = restaurant.Seats;
                res.TableName = restaurant.TableName;

                await _context.SaveChangesAsync();
                return "Base de datos actualizada";
            }
            catch
            {
                return "Error. Hubo un error al actualizar la base de datos";
            }
        }

        // DELETE: api/Schedules/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteTable(int id)
        {
            try
            {
                var reservations = await _context.Reservation.Where(i => i.Idtable == id).ToListAsync();
                if (reservations.Any())
                {
                    return "No se puede Eliminar. La mesa posee reservaciones";
                }

                // Eliminar sus estadisticas
                var stats = await _context.TableStatistics.Where(i => i.IDTable == id).ToListAsync();

                foreach (TableStatistics t in stats)
                {
                    _context.TableStatistics.Remove(t);
                    await _context.SaveChangesAsync();
                }

                // Eliminamos la mesa
                var res = await _context.RestaurantTables.Where(i => i.Idrestaurant == id).FirstAsync();
                _context.RestaurantTables.Remove(res);
                await _context.SaveChangesAsync();
                return "Base de datos actualizada";
            }
            catch
            {
                return "Error. Hubo un error al actualizar la base de datos";
            }
        }

    }
}