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
    public class CommentsController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public CommentsController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // GET: api/Comments
        // MAURICIO FARFAN
        // Verificador -> Reseñas
        // Method used to get all the comments that are in an 'verifier' status.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comments>>> GetComments()
        {
            return await _context.Comments.Where(c => c.Status == "Verifier").ToListAsync();
        }

        // GET: api/Comments/{idRestaurant}
        // MAURICIO FARFAN
        // Usuario -> RestaurantView -> Ver todas las reseñas.
        // Method used to get all the comments of a restaurant that are in an 'active' status.
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Comments>>> GetComments(int id)
        {
            var comments = await _context.Comments.Where(c => c.Idrestaurant == id && c.Status == "Aceptado").ToListAsync();

            if (comments == null)
                return NotFound();

            return comments;
        }

        // POST: api/Comments
        // MAURICIO FARFAN
        // Usuario -> Visitados -> Reseña -> Enviar datos
        // Method to post a comment for the restaurant.
        [HttpPost]
        public async Task<Comments> PostComments(Comments comments)
        {
            _context.Comments.Add(comments);
            await _context.SaveChangesAsync();
            return _context.Comments.Where(c => c.Idrestaurant == comments.Idrestaurant && c.Iduser == comments.Iduser
            && c.Comment == comments.Comment).FirstOrDefault();
        }

    }
}
