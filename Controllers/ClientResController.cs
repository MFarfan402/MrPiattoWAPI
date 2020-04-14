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
    public class ClientResController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public ClientResController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // GET: api/ClientRes/5
        [HttpGet("{idClient}")]
        public async Task<ActionResult<User>> GetClient(int idClient)
        {
            var client = await _context.User.Where(u => u.Iduser == idClient).FirstAsync();

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }
    }
}