using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrPiattoWAPI.Model;
using Newtonsoft.Json.Linq;

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

        // MAURICIO FARFAN
        [HttpGet("LogIn/{user}/{password}")]
        public async Task<Model.User> LogInUser(string user, string password)
        {
            var u = await _context.User.Where(u => u.Mail == user && u.Password == password).FirstOrDefaultAsync();
            return (u == null) ? null : u;
        }

        // POST: api/Users
        // MAURICIO FARFAN
        [HttpPost]
        public async Task<int> PostUser(Model.User user)
        {
            user.UnlockedDay = null;
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            var id = _context.User.Where(u => u.Mail == user.Mail).FirstOrDefault();
            return id.Iduser;
        }

        // POST: api/Users/Update
        // MAURICIO FARFAN
        [HttpPost("Update")]
        public async Task<bool> Update(Model.User user)
        {
            var oldUser = await _context.User.Where(u => u.Iduser == user.Iduser).FirstOrDefaultAsync();
            if (oldUser == null)
                return false;

            oldUser.FirstName = user.FirstName;
            oldUser.LastName = user.LastName;
            oldUser.Phone = user.Phone;
            oldUser.Gender = user.Gender;
            _context.User.Update(oldUser);
            await _context.SaveChangesAsync();
            return true;
        }

        [HttpGet("Intervals/{id}/{interval1}/{interval2}")]
        public async Task<ActionResult<IEnumerable<Model.User>>> GetUsersInterval(int id, DateTime interval1, DateTime interval2)
        {
            var users = await _context.Reservation
                .Where(r => r.IdtableNavigation.Idrestaurant == id
                && r.Date >= interval1 && r.Date <= interval2)
                .Select(u => u.IduserNavigation).ToListAsync();

            if (users == null)
                return NotFound();
            return users;
        }

        [HttpGet("All/{id}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAllUsers(int id)
        {
            var reservations = await _context.Reservation
                .Where(r => r.IdtableNavigation.Idrestaurant == id).ToListAsync();

            if (reservations == null)
                return NotFound();
            return reservations;
        }

        [HttpGet("Password/{mail}")]
        public async Task<string> SendMail(string mail)
        {
            var client = _context.User.Where(c => c.Mail == mail).FirstOrDefault();
            if (client == null)
                return "Error. El correo ingresado no tiene cuenta en MrPiatto.";
            
            client.Password = GeneratePassword();
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            await SendNewPassword(client.Mail, client.Password);
            return "Ha sido enviado un correo con la nueva contraseña.";
        }

        private async Task SendNewPassword(string mail, string password)
        {
            MailjetClient client = new MailjetClient("36b25d6cba30469cf1cc8911bf79d22a", "882892776ad1f8768b813bcc8e8358b0")
            { Version = ApiVersion.V3_1, };
            MailjetRequest request = new MailjetRequest { Resource = Send.Resource, }
            .Property(Send.Messages, new JArray {
            new JObject {
                { "From",
                    new JObject {
                        {"Email", "maufar402@gmail.com"},
                        {"Name", "MrPiatto Configuration Manager"}}
                }, { "To",
                    new JArray {
                        new JObject {
                        { "Email",
                          $"{mail}"
                        }, {
                          "Name",
                          "Ailem" }}}
                }, { "Subject", "Tu nueva contraseña de MrPiatto."
                }, { "TextPart","Sending Password"
                }, { "HTMLPart",
                    $"<h3>Lamentamos que hayas perdido tu contraseña.</h3><br/><br/>" +
                    $"No te preocupes, aquí esta la nueva contraseña:{password}<br/>" +
                    $"Favor de no responder a este correo."
                }, { "CustomID","AppGettingStartedTest"}}});
            MailjetResponse response = await client.PostAsync(request);
        }

        private string GeneratePassword()
        {
            Random random = new Random();
            return random.Next(10000, 99999).ToString();
        }
    }
}
