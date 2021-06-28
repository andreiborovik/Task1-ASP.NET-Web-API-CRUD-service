using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task1.Models;

namespace Task1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        TaskContext db;
        public CompaniesController(TaskContext taskContext)
        {
            db = taskContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> Get()
        {
            return await db.Companies.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> Get(int Id)
        {
            Company company = await db.Companies.FirstOrDefaultAsync(x => x.Id == Id);
            if (company == null)
                return NotFound();
            return new ObjectResult(company);
        }

        [HttpGet("{id}/users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUser(int Id)
        {
            Company company = await db.Companies.FirstOrDefaultAsync(x => x.Id == Id);
            if (company == null)
            {
                return BadRequest();
            }
            return company.Users;
        }

        [HttpPost]
        public async Task<ActionResult<Company>> Post(Company company)
        {
            if (company == null)
            {
                return BadRequest();
            }

            db.Companies.Add(company);
            await db.SaveChangesAsync();
            return Ok(company);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Company>> Delete (int Id)
        {
            Company company = await db.Companies.FirstOrDefaultAsync(x => x.Id == Id);
            if (company == null)
            {
                return BadRequest();
            }
            db.Companies.Remove(company);
            await db.SaveChangesAsync();
            return Ok(company);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Company>> Put(int id, Company company)
        {
            Company _company = await db.Companies.FirstOrDefaultAsync(x => x.Id == id);
            if (company == null)
            {
                return BadRequest();
            }
            _company = company;
            db.Companies.Update(_company);
            await db.SaveChangesAsync();
            return Ok(_company);
        }
    }
}
