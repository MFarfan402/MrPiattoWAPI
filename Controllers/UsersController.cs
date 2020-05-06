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
    public class UsersController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public UsersController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<User> PostUser(User user)
        //{
        //    var u = await _context.User.Where(u => u.Mail == user.Mail && u.Password == u.Password).FirstOrDefaultAsync();

        //    if (user == null)
        //    {
        //        return new User();
        //    }
        //    return u;
        //}

        //POST: api/Users
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<int> PostUser(User user)
        {
            user.UnlockedDay = null;
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            var id = _context.User.Where(u => u.Mail == user.Mail).FirstOrDefault();
            return id.Iduser;
        }
    }
}
