using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OPS_API.Domain.Models;
using OPS_API.Domain.Repositories;
using OPS_API.Persistence.Contexts;

namespace OPS_API.Persistence.Repositories
{
    public class OrganizationRepository : BaseRepository,IOrganizationRepository
    {
        public OrganizationRepository(ApplicationContext context) : base(context) { }

        public async Task<IEnumerable<Organization>> ListAllAsync()
        {
            return await _context.Organizations.ToListAsync();
        }

        public Task<IEnumerable<Organization>> FindAllAsync(Expression<Func<Organization, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Organization> FindAsync(Expression<Func<Organization, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(Expression<Func<Organization, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Organization> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Organization user)
        {
            throw new NotImplementedException();
        }
    }
}