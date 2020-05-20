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
        public async Task<int> PostUser(Model.User user)
        {
            user.UnlockedDay = null;
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            var id = _context.User.Where(u => u.Mail == user.Mail).FirstOrDefault();
            return id.Iduser;
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

        [HttpGet]
        public async Task<string> SendMail()
        {
            MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("36b25d6cba30469cf1cc8911bf79d22a"), 
                Environment.GetEnvironmentVariable("882892776ad1f8768b813bcc8e8358b0"))
            {
                Version = ApiVersion.V3_1,
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }.Property(Send.Messages, new JArray 
                {
                    new JObject 
                    {
                        {
                            "From",
                            new JObject 
                            {
                                {"Email", "maufar402@gmail.com"},
                                {"Name", "MAURICIO"}
                            }
                        }, 
                        {
                            "To",
                            new JArray 
                            {
                                new JObject 
                                {
                                    {
                                      "Email",
                                      "maufar402@gmail.com"
                                    }, 
                                    {
                                      "Name",
                                      "MAURICIO"
                                    }
                                }           
                            }
                        }, 
                        {
                           "Subject",
                           "Greetings from Mailjet."
                        }, 
                        {
                           "TextPart",
                           "My first Mailjet email"
                        }, 
                        {
                           "HTMLPart",
                           "<h3>Dear passenger 1, welcome to <a href='https://www.mailjet.com/'>Mailjet</a>!</h3><br />May the delivery force be with you!"
                        }, 
                        {
                           "CustomID",
                           "AppGettingStartedTest"
                        }
                    }
                });
            MailjetResponse response = await client.PostAsync(request);
            if(response.IsSuccessStatusCode)
            {
                return "El mensaje se ha enviado correctamente";
            }
            else
            {
                return $"StatusCode:  {response.StatusCode}\n" +
                    $"ErrorInfo: {response.GetErrorInfo()}\n" +
                    $"ErrorMessage: {response.GetErrorMessage()}";
            }
        }
    }
}
