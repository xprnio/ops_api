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
    public class RescuerRepository : BaseRepository, IRescuerRepository
    {
        public RescuerRepository(ApplicationContext context) : base(context) { }

        public async Task<IEnumerable<Rescuer>> ListAllAsync()
        {
            return await _context.Rescuers
                .Include(r => r.Operation)
                .Include(r => r.Organization)
                .ToListAsync();
        }

        public async Task<IEnumerable<Rescuer>> FindAllAsync(Expression<Func<Rescuer, bool>> predicate)
        {
            return await _context.Rescuers
                .Where(predicate)
                .Include(r => r.Operation)
                .Include(r => r.Organization)
                .ToListAsync();
        }

        public async Task<Rescuer> FindAsync(Expression<Func<Rescuer, bool>> predicate)
        {
            return await _context.Rescuers
                .Include(r => r.Operation)
                .Include(r => r.Organization)
                .SingleOrDefaultAsync(predicate);
        }

        public async Task<long> CountAsync(Expression<Func<Rescuer, bool>> predicate)
        {
            return await _context.Rescuers.CountAsync(predicate);
        }

        public async Task<Rescuer> FindByIdAsync(Guid id)
        {
            return await _context.Rescuers
                .Include(r => r.Operation)
                .Include(r => r.Organization)
                .SingleOrDefaultAsync(r => id.Equals(r.Id));
        }

        public async Task AddAsync(Rescuer user)
        {
            await _context.Rescuers.AddAsync(user);
        }

        public async Task<IEnumerable<string>> ListDistinctPhoneNumbersAsync()
        {
            return await _context.Rescuers.Select(n => n.PhoneNumber)
                .Distinct()
                .ToListAsync();
        }

        public async Task LoadOrganization(Rescuer rescuer)
        {
            await _context.Entry(rescuer)
                .Reference(r => r.Organization)
                .LoadAsync();
        }

        public async Task LoadOperation(Rescuer rescuer)
        {
            await _context.Entry(rescuer)
                .Reference(r => r.Operation)
                .LoadAsync();
        }
    }
}