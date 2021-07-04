using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task1.Validation;
using Task1.Models;
using Task1.Service;

namespace Task1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UserValidator userValidator;
        DatabaseService databaseService;
        public UsersController(TaskContext taskContext)
        {
            userValidator = new UserValidator();
            databaseService = new DatabaseService(taskContext);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
        {
            return await databaseService.GetUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(int Id)
        {
            User user = await databaseService.SearchUserAsync(Id); ;
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        [HttpGet("company")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserByCompanyAsync(Company company)
        {
            return await databaseService.GetUserByCompanyAsync(company);
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUserAsync(User user)
        {
            var userResult = userValidator.Validate(user);
            if (user == null || !userResult.IsValid)
            {
                return BadRequest();
            }
            await databaseService.AddUserAsync(user);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUserAsync(int Id)
        {
            User user = await databaseService.SearchUserAsync(Id); ;
            if (user == null)
            {
                return NotFound();
            }
            await databaseService.DeleteUserAsync(user);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUserAsync(int Id, User _user)
        {
            User user = await databaseService.SearchUserAsync(Id);
            var userResult = userValidator.Validate(_user);
            if (user == null || !userResult.IsValid)
            {
                return BadRequest();
            }
            user = _user;
            await databaseService.UpdateUserAsync(user);
            return Ok(user);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateCompanyOfUserAsync(int Id, Company company)
        {
            User user = await databaseService.SearchUserAsync(Id);
            if (user == null)
            {
                return BadRequest();
            }
            user.Company = company;
            await databaseService.UpdateUserAsync(user);
            return Ok(user);
        }
    }
}
