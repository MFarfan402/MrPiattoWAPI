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

        // GET: api/Comments/Delete/{idComment}
        // MAURICIO FARFAN
        // Verificador -> Reseñas -> Eliminar
        [HttpGet("Delete/{id}")]
        public async Task<bool> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                return false;

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        // GET: api/Comments/NotBad/{idComment}
        // MAURICIO FARFAN
        // Verificador -> Reseñas -> Eliminar
        [HttpGet("NotBad/{id}")]
        public async Task<bool> NotBadComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                return false;

            comment.Status = "Aceptado";
            _context.Entry(comment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        // GET: api/Comments/Report/{id}
        // MAURICIO FARFAN
        // Usuario -> RestaurantView -> Ver todas las reseñas -> Reportar
        [HttpGet("Report/{id}")]
        public async Task<bool> NoticeComments(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
                return false;

            comment.Status = "Verifier";
            _context.Entry(comment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
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
