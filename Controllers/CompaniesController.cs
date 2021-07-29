using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task1.Models;
using Task1.Service;
using Serilog;
using Task1.Filters;

namespace Task1.Controllers
{
    [MyExceptionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        DatabaseService databaseService;
        public CompaniesController(TaskContext taskContext)
        {
            databaseService = new DatabaseService(taskContext);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompaniesAsync()
        {
            return await databaseService.GetCompaniesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompanyByIdAsync(int Id)
        {
            Company company = await databaseService.SearchCompanyAsync(Id);
            if (company == null)
            {
                throw new System.Exception("Компания не найдена");
            }
            return new ObjectResult(company);
        }

        [HttpGet("{id}/users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersOfCompanyAsync(int Id)
        {
            Company company = await databaseService.SearchCompanyAsync(Id);
            if (company == null)
            {
                throw new System.Exception("Компания не найдена");
            }
            return company.Users;
        }

        [HttpPost]
        public async Task<ActionResult<Company>> PostCompanyAsync(Company company)
        {
            if (company == null)
            {
                throw new System.Exception("Компания не указана");
            }
            await databaseService.AddCompanyAsync(company);
            return Ok(company);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Company>> DeleteCompanyAsync(int Id)
        {
            Company company = await databaseService.SearchCompanyAsync(Id);
            if (company == null)
            {
                throw new System.Exception("Компания не найдена");
            }
            await databaseService.DeleteCompanyAsync(company);
            return Ok(company);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Company>> UpdateCompanyAsync(int Id, Company _company)
        {
            Company company = await databaseService.SearchCompanyAsync(Id);
            if (company == null)
            {
                throw new System.Exception("Компания не найдена");
            }
            company = _company;
            await databaseService.UpdateCompanyAsync(company);
            return Ok(_company);
        }
    }
}
