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
    public class RestaurantsController : ControllerBase
    {
        private readonly MrPiattoDB2Context _context;

        public RestaurantsController(MrPiattoDB2Context context)
        {
            _context = context;
        }

        // GET: api/Restaurants
        // MAURICIO FARFAN
        // Verficador -> Añadir
        // Method used to get all the possibles restaurants to MrPiattoService.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurant()
        {
            return await _context.Restaurant.Where(r => r.Confirmation == false).ToListAsync();
        }

        // GET: api/Restaurants/Deny/{idRestaurant}
        // MAURICIO FARFAN
        // Verficador -> Añadir -> Denegar
        [HttpGet("Deny/{id}")]
        public async Task<bool> DenyRestaurant(int id)
        {

            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
                return false;

            _context.Restaurant.Remove(restaurant);
            await _context.SaveChangesAsync();
            return true;
        }

        // GET: api/Restaurants/Accept/{idRestaurant}
        // MAURICIO FARFAN
        // Verficador -> Añadir -> Agregar
        [HttpGet("Accept/{id}")]
        public async Task<string> AcceptRestaurant(int id)
        {
            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
                return "Error. Intente de nuevo más tarde.";

            restaurant.Password = GeneratePassword();
            _context.Entry(restaurant).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            await AddGeneralWaiter(restaurant.Idrestaurant);
            await SetPoicies(restaurant.Idrestaurant);
            await SendNewPassword(restaurant.Mail ,restaurant.Password);

            return restaurant.Password;
        }

        private async Task SetPoicies(int idrestaurant)
        {
            Policies policies = new Policies();
            policies.MaxTimeRes = 7;
            policies.MaxTimePer = 2;
            policies.MinTimeRes = 1;
            policies.MinTimePer = 2;

            policies.ModTimeDays = 1;
            policies.ModTimeSeats = 0;
            policies.ModTimeHours = 8;

            policies.MaxTimeArr = 30;
            policies.MaxTimeArrPer = 0;
            policies.Strikes = true;
            policies.StrikeType = 7;
            policies.StrikeTypePer = 2;
            policies.Idrestaurant = idrestaurant;

            _context.Policies.Add(policies);
            await _context.SaveChangesAsync();
        }

        private async Task AddGeneralWaiter(int idRestaurant)
        {
            Waiters waiter = new Waiters();
            waiter.Idrestaurant = idRestaurant;
            waiter.WaiterFirstName = "Meseros";
            waiter.WaiterLasName = "General";
            _context.Waiters.Add(waiter);
            await _context.SaveChangesAsync();
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
                    $"<h3>Nos alegra que seas parte de MrPiatto.</h3><br/><br/>" +
                    $"Aquí esta tu nueva contraseña:{password}<br/>" +
                    $"Favor de no responder a este correo."
                }, { "CustomID","AppGettingStartedTest"}}});
            MailjetResponse response = await client.PostAsync(request);
        }

        private string GeneratePassword()
        {
            Random random = new Random();
            return random.Next(10000, 99999).ToString();
        }

        // GET: api/Restaurants/Inactive
        // MAURICIO FARFAN
        [HttpGet("Inactive")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetInactiveRestaurants()
        {
            var time = DateTime.Now - new TimeSpan(7, 0, 0, 0);
            List<Restaurant> restaurants = await _context.Restaurant.Where
                (r => r.LastLogin < time && r.Confirmation == true).ToListAsync();
            if (restaurants == null)
                return null;
            return restaurants;
        }

        // GET: api/Restaurants/DeleteInactive
        // MAURICIO FARFAN
        [HttpGet("DeleteInactive/{id}")]
        public async Task<bool> DeleteInactive(int id)
        {
            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
                return false;

            restaurant.Confirmation = false;
            _context.Entry(restaurant).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        // GET: api/Restaurants/{idRestaurant}
        // MAURICIO FARFAN
        // Usuario -> RestaurantView
        // Method used to get all the information about a restaurant.
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _context.Restaurant.Where(r => r.Idrestaurant == id)
                .Include(r => r.IdcategoriesNavigation)
                .Include(r => r.IdpaymentNavigation)
                .FirstAsync();

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }

        // GET: api/Restaurants/MainPage
        // MAURICIO FARFAN
        // Usuario -> HomeFragmentView
        // Method used to get all the information about a restaurant and put them into the fragment.
        [HttpGet("MainPage")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetUserMainPage()
        {
            Random random = new Random();
            var restaurants = await _context.Restaurant.Where(r => r.Confirmation == true)
                .Include(c=>c.IdcategoriesNavigation).Include(z => z.IdpaymentNavigation).ToListAsync();
            List<Restaurant> resOnScreen = new List<Restaurant>();
            for (int i = 0; i < 4;)
            {
                int index = random.Next(restaurants.Count);
                if (!resOnScreen.Contains(restaurants[index]))
                {
                    resOnScreen.Add(restaurants[index]);
                    i++;
                }
            }
            return resOnScreen;
        }

        // POST: api/Restaurants
        // MAURICIO FARFAN
        // Usuario -> Menu lateral -> Quiero ser Mr. Piatto
        // Method to put a restaurant into the system, with confirmation = 0.
        [HttpPost]
        public async Task<string> PostRestaurant(Restaurant restaurant)
        {
            restaurant.LastLogin = null;
            restaurant.Idcategories = null;
            restaurant.Idpayment = null;
            _context.Restaurant.Add(restaurant);
            await _context.SaveChangesAsync();
            return "Solicitud enviada. Espera la llamada de nuestro equipo!";
 }

        // GET: api/Restaurants/Verifier/{idRestaurant}
        // MAURICIO FARFAN
        // Verificador -> Seguimiento -> Barra de búsqueda
        // Method used to get all the information about a restaurant and put them into the fragment.
        [HttpGet("Verifier/{id}")]
        public async Task<ActionResult<Restaurant>> VerifierSearch(int id)
        {
            var restaurant = await _context.Restaurant.Where(r => r.Idrestaurant == id)
               .Include(r => r.IdcategoriesNavigation)
               .Include(r => r.IdpaymentNavigation)
               .FirstAsync();

            if(restaurant == null)
            {
                return NotFound();
            } 
            return restaurant;
        }

        // GET: api/Restaurants/Photos/{idRestaurant}
        // MAURICIO FARFAN
        [HttpGet("Photos/{id}")]
        public async Task<ActionResult<IEnumerable<string>>> GetUrlPhotos(int id)
        {
            var urlPhotos = await _context.RestaurantPhotos.Where(p => p.Idrestaurant == id).ToListAsync();
            if (urlPhotos == null) return null;
            List<string> url = new List<string>();
            foreach (var u in urlPhotos)
                url.Add(u.Url);
            return url;
        }
    }
}
