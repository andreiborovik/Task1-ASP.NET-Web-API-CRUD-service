using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1.Models;

namespace Task1.Service
{
    public class DatabaseService
    {
        TaskContext _db;
        public DatabaseService (TaskContext db)
        {
            _db = db;
        }

        public async Task AddUserAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }

        public async Task<User> SearchUserAsync(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<List<User>> GetUserByCompanyAsync(Company company)
        {
            return await _db.Users.Where(x => x.Company == company).ToListAsync();
        }

        public async Task<List<Company>> GetCompaniesAsync()
        {
            return await _db.Companies.ToListAsync();
        }

        public async Task<Company> SearchCompanyAsync(int id)
        {
            return await _db.Companies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddCompanyAsync(Company company)
        {
            _db.Companies.Add(company);
            await _db.SaveChangesAsync();    
        }

        public async Task DeleteCompanyAsync(Company company)
        {
            _db.Companies.Remove(company);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateCompanyASync(Company company)
        {
            _db.Companies.Update(company);
            await _db.SaveChangesAsync();
        }
    }
}
