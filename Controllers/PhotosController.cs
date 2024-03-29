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
    public class PhotosController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public PhotosController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // GET: api/ClientRes/5
        [HttpGet("Restaurant/{idRes}")]
        public async Task<ActionResult<IEnumerable<RestaurantPhotos>>> GetDayStatistics(int idRes)
        {
            var restaurantPhotos = await _context.RestaurantPhotos.Where(r => r.Idrestaurant == idRes).ToListAsync();

            if (restaurantPhotos == null)
            {
                return NotFound();
            }

            return restaurantPhotos;
        }

        [HttpGet("Delete/Photos/{idPhoto}")]
        public async Task<string> DeletePhotos(int idPhoto)
        {
            try
            {
                var photo = await _context.RestaurantPhotos
                    .Where(p => p.IdrestaurantPhotos == idPhoto).FirstAsync();

                _context.RestaurantPhotos.Remove(photo);
                await _context.SaveChangesAsync();

                return "Foto eliminado";

            }
            catch
            {
                return "Error. Hubo un error con el servidor";
            }
        }
    }
}