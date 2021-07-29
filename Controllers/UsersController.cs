using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task1.Validation;
using Task1.Models;
using Task1.Service;
using Task1.Filters;
using System;

namespace Task1.Controllers
{
    [MyExceptionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UserValidator userValidator;
        DatabaseService databaseService;
        public UsersController(UserValidator _userValidator, DatabaseService _databaseService)
        {
            databaseService = _databaseService;
            userValidator = _userValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
        {
            return await databaseService.GetUsersAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(int Id)
        {
            User user = await databaseService.SearchUserAsync(Id);
            if (user == null) {
                throw new Exception("Пользователь не найден");
            }
            return new ObjectResult(user);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserByCompanyAsync(string Name)
        {
            return await databaseService.GetUserByCompanyAsync(Name);
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUserAsync(User user)
        {
            var userResult = userValidator.Validate(user);
            if (user == null || !userResult.IsValid)
            {
                throw new Exception("Неверный ввод данных");
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
                throw new Exception("Пользователь не найден");
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
                throw new Exception("Неверный ввод данных");
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
                throw new Exception("Пользователь не найден");
            }
            user.Company = company;
            await databaseService.UpdateUserAsync(user);
            return Ok(user);
        }
    }
}
