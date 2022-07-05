using AuthWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthWebAPI.Controllers
{
    [ApiController]
    [Route("api/create")]
    public class CreateController : ControllerBase
    {

        PersonContext db;
        public CreateController(PersonContext context)
        {
            db = context;
        }

 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            return await db.Persons.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Person>> Register(Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }

            person.Role = "user";
            db.Persons.Add(person);
            await db.SaveChangesAsync();
            return Ok(person);
        }
    }
}
