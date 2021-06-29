using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1.Validation;
using Task1.Models;

namespace Task1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        TaskContext db;
        UserValidator userValidator;
        public UsersController(TaskContext taskContext)
        {
            db = taskContext;
            userValidator = new UserValidator();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await db.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int Id)
        {
            User user = await db.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        [HttpGet("company")]
        public async Task<ActionResult<IEnumerable<User>>> GetCompany(Company company)
        {
            return await db.Users.Where(x => x.Company == company).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            var userResult = userValidator.Validate(user);
            if (user == null || !userResult.IsValid)
            {
                return BadRequest();
            }
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, User _user)
        {
            User user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            var userResult = userValidator.Validate(_user);
            if (user == null || !userResult.IsValid)
            {
                return BadRequest();
            }
            user = _user;
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
        //[HttpPut("{id}")]
        //public async Task<ActionResult<User>> Put(int id, Company company)
        //{
        //    User user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
        //    if (user == null)
        //    {
        //        return BadRequest();
        //    }
        //    user.Company = company;
        //    db.Users.Update(user);
        //    await db.SaveChangesAsync();
        //    return Ok(user);
        //}
    }
}
