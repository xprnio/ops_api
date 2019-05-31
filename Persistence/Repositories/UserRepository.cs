using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OPS_API.Domain.Models;
using OPS_API.Domain.Repositories;
using OPS_API.Persistence.Contexts;

namespace OPS_API.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context) { }

        public Task<IEnumerable<User>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> FindAllAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<IEnumerable<string>> ListAllPhoneNumbers()
        {
            return await _context.Users
                .Select(u => u.PhoneNumber)
                .Distinct()
                .ToListAsync();
        }
    }
}