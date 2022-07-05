using AuthWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : Controller

    {

        PersonContext db;

        public ValuesController(PersonContext context)
        {
            db = context;
        }

        [Authorize]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"YourLogin: {User.Identity.Name}");
        }

        [Authorize(Roles = "admin")]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok("Your role is: admin");
        }

        [Authorize(Policy = "OnlyForXanart")]
        [Route("getabout")]
        public IActionResult GetAbout()
        {
            return Ok("Only for Xanart");
        }


        [Authorize(Roles = "admin")]
        [Route("getallinfo")]
        public async Task<IActionResult> GetUsers()
        {
            List<Person> persons = await db.Persons.ToListAsync();
            string users = "";
            foreach (Person p in persons)
            {
                users += $"Id: {p.Id}, Login: {p.Login}, Password {p.Password}, Role {p.Role}\n";

            }
            return Ok($"Users: {users}");
        }


        [Authorize(Roles = "admin")]
        [Route("getspecificinfo/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            Person person = await db.Persons.FirstOrDefaultAsync(x => x.Id == id);
          
            return Ok($"Login: {person.Login}, Password {person.Password}, Role {person.Role}");
        }
    }
}
